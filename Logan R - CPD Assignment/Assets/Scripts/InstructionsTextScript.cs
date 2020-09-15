/*-----------------------------------------------------------
    File name: InstructionsTextScript.cs
    Purpose: Allow the player to go through the instructions.
    Author: Logan Ryan
    Modified: 15 September 2020
-------------------------------------------------------------
    Copyright 2020 Logan Ryan
-----------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsTextScript : MonoBehaviour
{
    public GameObject[] instructionTexts;

    private int index = 0;

    // Next is a function that will move to the next instruction page
    public void Next()
    {
        // If the player is not at the end of the instructions
        if (index != instructionTexts.Length - 1)
        {
            // Move to the next instruction page
            index++;
            instructionTexts[index].SetActive(true);
            instructionTexts[index - 1].SetActive(false);
        }
    }

    // Prev is a function that will move to the previous instruction page
    public void Prev()
    {
        // If the player is not at the start of the instructions
        if (index != 0)
        {
            // Move to the previous instruction page
            index--;
            instructionTexts[index].SetActive(true);
            instructionTexts[index + 1].SetActive(false);
        }
    }
}
