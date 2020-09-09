using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public static bool GameIsOver = false;
    
    public GameObject gameOverUI;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().isDead)
        {
            gameOverUI.SetActive(true);
        }
    }

    public void Retry()
    {
        gameOverUI.SetActive(false);
        GameIsOver = false;
        player.GetComponent<PlayerController>().isDead = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
