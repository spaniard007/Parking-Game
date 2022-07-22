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

    public bool isMovingBack;
    
    // Start is called before the first frame update
    void Start()
    {
        movementDirection = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AttemptToMove(Vector2 dragVal)
    {
        if (transform.eulerAngles.y == 90f) // left faced
        {
            Debug.Log(transform.rotation.eulerAngles.y);
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

    private void FixedUpdate()
    {
        transform.position = transform.position + movementDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "obstacles")
        {
            //obstacle effect later
            movementDirection *= -1;
            Invoke("StopVehicle",0.1f);

        }
        else if (other.gameObject.tag == "vehicles")
        {
            movementDirection *= -1;
            Invoke("StopVehicle",0.1f);
        }
    }

    private void StopVehicle()
    {
        vehicleState = VehicleState.In_Parking;
        movementDirection = Vector3.zero;
    }

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
            }
        }
    }
}
