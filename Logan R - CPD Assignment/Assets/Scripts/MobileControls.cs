/*----------------------------------------------------------------------
    File name: MobileControls.cs
    Purpose: Show mobile controls if the game is being played on mobile.
    Author: Logan Ryan
    Modified: 16 September 2020
------------------------------------------------------------------------
    Copyright 2020 Logan Ryan
----------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    public GameObject joystick;
    public GameObject jumpButton;
    public GameObject attackButton;
    public GameObject pauseButton;

    // Update is called once per frame
    void Update()
    {
        // If the game is being played on mobile, show touch controls
        if (Application.isMobilePlatform)
        {
            joystick.SetActive(true);
            jumpButton.SetActive(true);
            attackButton.SetActive(true);
            pauseButton.SetActive(true);
        }
    }
}
