/*--------------------------------------
    File name: GameOverMenu.cs
    Purpose: Control the game over menu.
    Author: Logan Ryan
    Modified: 15 September 2020
----------------------------------------
    Copyright 2020 Logan Ryan
--------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverUI;
    public PlayerController playerController;
    public GameObject startButton;
    public GameObject quitButton;

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            quitButton.SetActive(false);
        }

        // If the player is dead
        if (playerController.isDead)
        {
            // Load the game over screen
            gameOverUI.SetActive(true);
        }

        // If there is no selected button
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            // Set the selected button to be the chosen start button
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startButton);
        }
    }

    // Retry is the function to let the player have another go at the level
    public void Retry()
    {
        // Unload the game over screen
        gameOverUI.SetActive(false);

        // Player is no longer dead
        playerController.isDead = false;

        // Reload the scene
        if (Checkpoint.GetActiveCheckPointPosition() != Vector3.zero || Checkpoint.GetActiveCheckPointPosition() != null)
        {
            Debug.Log("Spawning at checkpoint");
            Vector3 startPos = Checkpoint.GetActiveCheckPointPosition();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            playerController.robo3CharacterController.transform.position = startPos;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // LoadMenu is the function to let the player go back to the main menu
    public void LoadMenu()
    {
        // Load the Main Menu Scene
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    // QuitGame is th function to let the player quit the game
    public void QuitGame()
    {
        // Quit application
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
