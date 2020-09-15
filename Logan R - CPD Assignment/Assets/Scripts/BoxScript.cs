/*--------------------------------------------------------
    File name: BoxScript.cs
    Purpose: Play special effects for the breakable boxes.
    Author: Logan Ryan
    Modified: 15 September 2020
----------------------------------------------------------
    Copyright 2020 Logan Ryan
--------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public ParticleSystem smokeParticles;
    public AudioClip brokenBoxSoundEffect;

    // OnDestroy is called when the MonoBehaviour will be destroyed
    public void OnDestroy()
    {
        // Play the broken box sound effect at it's current position
        AudioSource.PlayClipAtPoint(brokenBoxSoundEffect, transform.position);

        // Add smoke particles prefab to the box object
        Instantiate(smokeParticles, transform.position, smokeParticles.transform.rotation);

        // Play the smoke particle effect
        smokeParticles.Play();
    }
}
