/*-----------------------------------------------------------
    File name: PauseMenu.cs
    Purpose: Bring up the pause menu and navigate through it.
    Author: Logan Ryan
    Modified: 16 September 2020
-------------------------------------------------------------
    Copyright 2020 Logan Ryan
-----------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject startButton;
    public PlayerController playerController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !playerController.isDead)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Resume is called when the player wants to continue playing the game
    public void Resume()
    {
        EventSystem.current.SetSelectedGameObject(null);
        pauseMenuUI.SetActive(false);

        // Resume the time scale
        Time.timeScale = 1f;

        GameIsPaused = false;
    }

    // Pause is called to stop the game
    public void Pause()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
        pauseMenuUI.SetActive(true);

        // Stop the game in the background
        Time.timeScale = 0f;

        GameIsPaused = true;
    }

    // LoadMenu loads the main menu
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");

        // Resume the time scale
        Time.timeScale = 1f;
    }

    // QuitGame is called if the player wants to quit the game
    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
