using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsTextScript : MonoBehaviour
{
    public GameObject[] instructionTexts;
    private int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Next()
    {
        if (index != instructionTexts.Length - 1)
        {
            index++;
            instructionTexts[index].SetActive(true);
            instructionTexts[index - 1].SetActive(false);
        }
    }

    public void Prev()
    {
        if (index != 0)
        {
            index--;
            instructionTexts[index].SetActive(true);
            instructionTexts[index + 1].SetActive(false);
        }
    }
}
