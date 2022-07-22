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

    public bool isMovingBack;
    
    // Start is called before the first frame update
    void Start()
    {
        movementDirection = Vector3.zero;
        vehicleState = VehicleState.In_Parking;
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
            transform.DOLocalMove(transform.position+movementDirection/2f, 0.05f).OnComplete(() =>
            {
                StopVehicle();
            });
        }
        else if (other.gameObject.tag == "vehicles")
        {
            isInCollider = true;
            movementDirection *= -1;
            transform.DOLocalMove(transform.position+movementDirection/2f, 0.05f).OnComplete(() =>
            {
                StopVehicle();
            });
        }
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
                        movementDirection = transform.forward;
                    }
                );
            }
        }
        else if (vehicleState == VehicleState.DrivingAway)
        {
            if (other.tag == "endPoint")
            {
                Destroy(gameObject);
                GameManager.instance.DeleteVehicle(this);
            }
        }
    }
    
    #endregion
}
