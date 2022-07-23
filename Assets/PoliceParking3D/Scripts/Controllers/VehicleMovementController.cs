using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum VehicleState
{
    In_Parking,
    Moving_In_Parking,
    Rotating,
    DrivingAway
}

public class VehicleMovementController : MonoBehaviour
{
    public int speed;
    private Vector3 movementDirection;
    public VehicleState vehicleState;
    private bool isInCollider;

    [SerializeField] private ParticleSystem driveAwaySmokeParticle;
    [SerializeField] private ParticleSystem backDustParticle;
    [SerializeField] private ParticleSystem frontDustParticle;
    [SerializeField] private Light sirenLight;
    
    
    public bool isMovingBack;
    
    // Start is called before the first frame update
    void Start()
    {
        movementDirection = Vector3.zero;
        vehicleState = VehicleState.In_Parking;
        sirenLight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region MovementAttempt
    
    public void AttemptToMove(Vector2 dragVal)
    {
        if(isInCollider)
            return;
        if (transform.eulerAngles.y == 90f) // left faced
        {
            movementDirection = transform.forward;
            if (dragVal.x < 0)
            {
                movementDirection *= -1;
            }
        }
       else if (transform.eulerAngles.y == 180f) // top faced
        {
            movementDirection = transform.forward;
            if (dragVal.y > 0)
            {
                movementDirection *= -1;
            }
        }
        else if (transform.eulerAngles.y == 270f) // right faced
        {
            movementDirection = transform.forward;
            if (dragVal.x > 0)
            {
                movementDirection *= -1;
            }
        }

        else if (transform.eulerAngles.y < 10f)  // down faced
        {
            movementDirection = transform.forward;
            if (dragVal.y < 0)
            {
                movementDirection *= -1;
            }
        }

        vehicleState = VehicleState.Moving_In_Parking;
        isMovingBack = movementDirection != transform.forward;
        AudioController.instance.VehicleMovementSound(this,vehicleState,true);
    }

    #endregion

    #region VehicleMovementMechanism
    
    private void FixedUpdate()
    {
        if(isInCollider)
            return;
        transform.position = transform.position + movementDirection * speed * Time.deltaTime;
    }

    private void StopVehicle()
    {
        vehicleState = VehicleState.In_Parking;
        movementDirection = Vector3.zero;
        isInCollider = false;
        AudioController.instance.VehicleMovementSound(this,vehicleState,false);
    }
    #endregion

    #region CollisionEnterExit
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "obstacles")
        {
            isInCollider = true;
            //obstacle effect later
            movementDirection *= -1;
            other.gameObject.GetComponent<Obstacles>().Animate(movementDirection);
            AudioController.instance.PlayObstacleHitSound();
            if (vehicleState == VehicleState.Moving_In_Parking)
            {
                if (isMovingBack)
                {
                    backDustParticle.Play();
                }
                else
                {
                    frontDustParticle.Play();
                }
            }

            transform.DOMove(transform.position+movementDirection/2f, 0.05f).OnComplete(() =>
            {
                StopVehicle();
            });
        }
        else if (other.gameObject.tag == "vehicles")
        {
            isInCollider = true;
            movementDirection *= -1;
            AudioController.instance.PlayObstacleHitSound();
            AudioController.instance.PlayHornSound();
            other.gameObject.GetComponent<VehicleMovementController>().CollisionWithOtherVehicle();
            if (vehicleState == VehicleState.Moving_In_Parking)
            {
                if (isMovingBack)
                {
                    backDustParticle.Play();
                }
                else
                {
                    frontDustParticle.Play();
                }
            }

            transform.DOMove(transform.position+movementDirection/2f, 0.05f).OnComplete(() =>
            {
                StopVehicle();
            });
        }
    }
    
    
    #endregion


    #region VehicleEffects
    
    public void CollisionWithOtherVehicle()
    {
        if (vehicleState == VehicleState.In_Parking)
        {
            float duration = 0.1f;
            float strength = 0.1f;

            transform.DOShakePosition(duration, strength);
            transform.DOShakeRotation(duration, strength);
        }
    }

    private void TurnOnSirenLight()
    {
        sirenLight.gameObject.SetActive(true);
        sirenLight.DOColor(Color.red, 0.2f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    #endregion
    
    #region ColllisionTrigger
    
    private void OnTriggerEnter(Collider other)
    {
        if (vehicleState == VehicleState.Moving_In_Parking)
        {
            if (other.tag == "road")
            {
                vehicleState = VehicleState.Rotating;
                movementDirection = Vector3.zero;
                transform.DORotate(new Vector3(0, transform.eulerAngles.y + 90, 0), 0.1f).OnComplete(() =>
                    {
                        vehicleState = VehicleState.DrivingAway;
                        AudioController.instance.VehicleMovementSound(this,vehicleState,true);
                        movementDirection = transform.forward;
                        TurnOnSirenLight();
                        driveAwaySmokeParticle.Play();
                    }
                );
            }
        }
        else if (vehicleState == VehicleState.DrivingAway)
        {
            if (other.tag == "endPoint")
            {
                GameManager.instance.DeleteVehicle(this);
                Destroy(gameObject);
                if (GameManager.instance.vehicles.Count ==0)
                {
                    GameManager.instance.LevelComplete();
                }
            }
        }
    }
    
    #endregion
}
