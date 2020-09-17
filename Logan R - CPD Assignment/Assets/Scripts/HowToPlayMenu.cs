/*--------------------------------------
    File name: HowToPlayMenu.cs
    Purpose: Control the game over menu.
    Author: Logan Ryan
    Modified: 15 September 2020
----------------------------------------
    Copyright 2020 Logan Ryan
--------------------------------------*/

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayMenu : MonoBehaviour
{
    private string[] controllers;

    public GameObject mainMenu;
    public GameObject startButton;
    public GameObject keyboardControls;
    public GameObject joystickControls;
    public GameObject touchControls;

    public GameObject switchButton;
    public GameObject controlsMenu;
    public GameObject instructionsMenu;

    public GameObject player;
    public GameObject pole;

    // Update is called once per frame
    void Update()
    {
        // If game is being played on mobile
        if (Application.isMobilePlatform)
        {
            // Show the touch controls
            touchControls.SetActive(true);
            keyboardControls.SetActive(true);
        }
        else
        {
            // Check if a controller is connected
            controllers = Input.GetJoystickNames();

            // If there is no controllers connected
            if (controllers.Length == 0)
            {
                // Show keyboard controls
                keyboardControls.SetActive(true);
                joystickControls.SetActive(false);
            }

            // Check if the controller is still connected
            for (int i = 0; i < controllers.Length; i++)
            {
                // If it is still connected
                if (!String.IsNullOrEmpty(controllers[i]))
                {
                    // Show joystick controls
                    keyboardControls.SetActive(false);
                    joystickControls.SetActive(true);
                }
                else
                {
                    // Show keyboard controls
                    keyboardControls.SetActive(true);
                    joystickControls.SetActive(false);
                }
            }
        }
    }

    // Switch is a function that switches between the controls menu and instruction menu
    public void Switch()
    {
        // If the control menu is active
        if (controlsMenu.activeSelf)
        {
            // Switch to instruction menu
            switchButton.GetComponentInChildren<TextMeshProUGUI>().text = "CONTROLS";
            instructionsMenu.SetActive(true);
            controlsMenu.SetActive(false);
        }
        // If the instructions menu is active
        else if (instructionsMenu.activeSelf)
        {
            // Switch to controls menu
            switchButton.GetComponentInChildren<TextMeshProUGUI>().text = "INSTRUCTIONS";
            controlsMenu.SetActive(true);
            instructionsMenu.SetActive(false);
        }
    }

    // Back is a function that return the player to the main menu
    public void Back()
    {
        // Set the main menu to active
        mainMenu.SetActive(true);
        player.SetActive(true);
        pole.SetActive(true);

        // Set the selected button to be the chosen start button
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);

        // Set the how to play menu to inactive
        gameObject.SetActive(false);
    }
}
