/*-----------------------------------------------
    File name: CoinScript.cs
    Purpose: Play the sound effect for the coins.
    Author: Logan Ryan
    Modified: 15 September 2020
-------------------------------------------------
    Copyright 2020 Logan Ryan
-----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public AudioClip coinCollectedSoundEffect;
    
    // Update is called once per frame
    void Update()
    {
        // Rotate 1 degree every frame
        transform.Rotate(Vector3.up, 1.0f, Space.World);
    }

    // OnDestroy is called when the MonoBehaviour will be destroyed
    private void OnDestroy()
    {
        // Play the coin collected sound effect
        AudioSource.PlayClipAtPoint(coinCollectedSoundEffect, transform.position);
    }
}
