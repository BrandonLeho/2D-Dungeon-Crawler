using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    public Transform pivotPoint; // Drag and drop the pivot point GameObject in the Inspector
    public float targetRotation = 135f; // Adjust as needed
    public float tempTargetRotation = 0f;
    public float rotationSpeed = 1000f; // Adjust as needed

    private bool isRotating = false;
    public int rotationDirection = 1; // 1 for counter-clockwise, -1 for clockwise

    private float timeElapsed = 0f;
    private bool waitForUserInput = false;

    public Transform Aim;
    
    private Quaternion initialRotation;

    private void Update()
    {
        
        // Check if the pivot point is assigned
        if (pivotPoint != null)
        {
            if (!isRotating)
            {
                // Check for mouse click to start rotation
                if (Input.GetMouseButtonDown(0))
                {
                    //Get mouse position
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
                    //Direction from us to mouse
                    Vector3 shootDirection = (mousePosition - transform.position).normalized;

                    initialRotation = Aim.rotation = Quaternion.LookRotation(Vector3.forward, -shootDirection);

                    targetRotation = 135f;
                    targetRotation -= initialRotation.eulerAngles.z;

                    isRotating = true;
                    // Change rotation direction on each click
                    rotationDirection *= -1;
                }
            }
            else
            {
                // Rotate the sprite around the pivot point
                transform.RotateAround(pivotPoint.position, Vector3.forward, rotationDirection * rotationSpeed * Time.deltaTime);

                // Check if the target rotation is reached in either direction
                float currentRotation = transform.rotation.eulerAngles.z - initialRotation.eulerAngles.z;
                if (rotationDirection == 1 && currentRotation >=  initialRotation.eulerAngles.z || (rotationDirection == -1 && currentRotation <= targetRotation))
                {
                    if (!waitForUserInput)
                    {
                        // Stop rotating and reset rotation to the target
                        isRotating = false;
                        waitForUserInput = true;
                        timeElapsed = 0f;
                    }
                }
            }

            // Check for user input during the delay
            if (waitForUserInput)
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= 1f)
                {
                    // Time limit reached, reset to initial position
                    transform.rotation = initialRotation;
                    rotationDirection = 1;
                    waitForUserInput = false;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    // User clicked within the time limit, rotate back to initial position
                    isRotating = true;
                    waitForUserInput = false;
                }
            }
        }
        else
        {
            Debug.LogError("Pivot point not assigned!");
        }
    }
}
