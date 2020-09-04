using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateScore : MonoBehaviour
{
    public PlayerController player;
    public Text scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreText.text = "Coins: " + player.playerCoins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
