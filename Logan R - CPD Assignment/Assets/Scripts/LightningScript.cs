/*------------------------------------------
    File name: LightningScript.cs
    Purpose: Switch the lighning on and off.
    Author: Logan Ryan
    Modified: 16 September 2020
--------------------------------------------
    Copyright 2020 Logan Ryan
------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public ParticleSystem[] lightningParticles;
    public float waitTime = 3;

    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        // Increase the timer by 1
        timer += 1 * Time.deltaTime;

        // If the timer is greater than the waitTime
        if (timer >= waitTime)
        {
            // Go through all the lightning particles on the electric obstacle
            for (int i = 0; i < lightningParticles.Length; i++)
            {
                // Start the shoot lightning coroutine
                StartCoroutine(ShootLightning(i));
            }

            // Disable the box collider
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            // Otherwise, enable the box collider
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    /// <summary>
    /// ShootLightning is a coroutine that switches the lightning on and off
    /// </summary>
    /// <param name="index"> A value greater than 0. </param>
    /// <returns> yield return new WaitForSeconds will make the coroutine stop for a couple of seconds then continue. </returns>
    IEnumerator ShootLightning(int index)
    {
        // Switch the lightning off
        lightningParticles[index].gameObject.SetActive(false);

        // Wait the waitTime
        yield return new WaitForSeconds(waitTime);

        // Switch the lightning on and reset the timer
        lightningParticles[index].gameObject.SetActive(true);
        timer = 0;
    }
}
