/*---------------------------------------------------------
    File name: MovingPlatformScript.cs
    Purpose: Make the platform move in the x and/or z axis.
    Author: Logan Ryan
    Modified: 16 September 2020
-----------------------------------------------------------
    Copyright 2020 Logan Ryan
---------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public float speed = 10.0f;
    public float xRange = 0;
    public float zRange = 0;

    public Vector3 xDirection = Vector3.zero;
    public Vector3 zDirection = Vector3.zero;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // FixedUpdate is called every fixed framerate frame, if the MonoBehaviour is enabled
    void FixedUpdate()
    {
        // If the platform is moving on the x axis
        if (xRange != 0)
        {
            // Set diretion to right if it has no direction
            if (xDirection == Vector3.zero)
            {
                xDirection = Vector3.right;
            }
            
            // If the platform has reach the right edge of its range
            if (transform.position.x > (startPos.x + xRange))
            {
               // Set direction to left
                xDirection = -Vector3.right;
            }
            // If the platform has reach the left edge of its range
            else if (transform.position.x < (startPos.x - xRange))
            {
                // Set direction to right
                xDirection = Vector3.right;
            }

            // Move platform
            transform.Translate(xDirection * speed * Time.fixedDeltaTime);
        }

        // If the platform is moving on the z axis
        if (zRange != 0)
        {
            // Do the same procedure as above but the platform moves forwards and backwards
            if (zDirection == Vector3.zero)
            {
                zDirection = Vector3.forward;
            }

            if (transform.position.z > (startPos.z + zRange))
            {
                zDirection = -Vector3.forward;
            }
            else if (transform.position.z < (startPos.z - zRange))
            {
                zDirection = Vector3.forward;
            }

            transform.Translate(zDirection * speed * Time.fixedDeltaTime);
        }
    }
}
