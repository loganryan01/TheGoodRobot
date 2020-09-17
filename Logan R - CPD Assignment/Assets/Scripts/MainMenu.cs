/*---------------------------------------
    File name: MainMenu.cs
    Purpose: Manage the main menu screen.
    Author: Logan Ryan
    Modified: 16 September 2020
-----------------------------------------
    Copyright 2020 Logan Ryan
---------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject howToPlayMenu;
    public GameObject startButton;

    public GameObject player;
    public GameObject pole;

    public GameObject quitButton;

    // Update is called once per frame
    private void Update()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            quitButton.SetActive(false);
        }
    }

    // PlayGame loads the next scene
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // HowToPlay switches the how to play menu
    public void HowToPlay()
    {
        howToPlayMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
        gameObject.SetActive(false);
        player.SetActive(false);
        pole.SetActive(false);
    }

    // QuitGame quits the game
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
