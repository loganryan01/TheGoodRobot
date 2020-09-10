using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverUI;
    public GameObject player;
    public GameObject startButton;

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().isDead)
        {
            gameOverUI.SetActive(true);
        }

        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(startButton);
        }
    }

    public void Retry()
    {
        gameOverUI.SetActive(false);
        player.GetComponent<PlayerController>().isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
