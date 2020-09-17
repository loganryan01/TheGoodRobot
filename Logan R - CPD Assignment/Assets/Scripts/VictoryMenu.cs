/*----------------------------------------------
    File name: VictoryMenu.cs
    Purpose: Give the buttons OnClick functions.
    Author: Logan Ryan
    Modified: 16 September 2020
------------------------------------------------
    Copyright 2020 Logan Ryan
----------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public GameObject quitButton;

    // Update is called once per frame
    private void Update()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            quitButton.SetActive(false);
        }
    }

    // PlayGame allows the player to play the game again
    public void PlayGame()
    {
        // Load previous scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    // QuitGame allows the player to quit the game
    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
