/*---------------------------------------------------------------
    File name: DroppingPlatformScript.cs
    Purpose: Make a platform drop after a certain amount of time.
    Author: Logan Ryan
    Modified: 15 September 2020
-----------------------------------------------------------------
    Copyright 2020 Logan Ryan
---------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatformScript : MonoBehaviour
{
    public float dropSpeed = 10.0f;
    public float waitTime = 1.5f;
    
    public bool playerOnPlatform;

    private bool shouldDrop;

    // Update is called once per frame
    void Update()
    {
        // If the platform should drop
        if (shouldDrop)
        {
            // Move down at a certain speed every second
            transform.Translate(-Vector3.up * dropSpeed * Time.deltaTime);
        }

        // If the player is on the platform
        if (playerOnPlatform)
        {
            // Start the wait time to drop the platform
            StartCoroutine(Drop());
        }
    }

    // Drop is called when the player is on the platform
    IEnumerator Drop()
    {
        playerOnPlatform = false;

        // Wait 5 seconds
        yield return new WaitForSeconds(waitTime);
        
        // Then start the drop process
        shouldDrop = true;
    }
}
