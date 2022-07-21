using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
        Debug.Log(transform.rotation.eulerAngles.y);
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
            Debug.Log(transform.rotation.eulerAngles.y);
            movementDirection = transform.forward;
            if (dragVal.y > 0)
            {
                movementDirection *= -1;
            }
        }
        else if (transform.eulerAngles.y == 270f) // right faced
        {
            Debug.Log(transform.rotation.eulerAngles.y);
            movementDirection = transform.forward;
            if (dragVal.x > 0)
            {
                movementDirection *= -1;
            }
        }

        else if (transform.eulerAngles.y < 10f)  // down faced
        {
            Debug.Log(transform.rotation.eulerAngles.y);
            movementDirection = transform.forward;
            if (dragVal.y < 0)
            {
                movementDirection *= -1;
            }
        }
        /*else //up down faced car
        {
            movementDirection = transform.forward;
            if(dragVal.x>0)
            {
                movementDirection *= -1;
            }
        }*/

        
    }

    private void FixedUpdate()
    {
        transform.position = transform.position + movementDirection * speed * Time.deltaTime;
    }
}
