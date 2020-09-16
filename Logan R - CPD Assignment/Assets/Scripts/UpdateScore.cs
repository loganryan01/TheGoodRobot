/*---------------------------------
    File name: UpdateScore.cs
    Purpose: Update the score text.
    Author: Logan Ryan
    Modified: 16 September 2020
-----------------------------------
    Copyright 2020 Logan Ryan
---------------------------------*/

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
        // Update the coin text when the player collects a coin
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreText.text = "Coins: " + player.playerCoins;
    }
}
