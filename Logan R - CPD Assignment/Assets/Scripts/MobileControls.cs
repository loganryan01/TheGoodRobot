using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    public GameObject joystick;
    public GameObject jumpButton;
    public GameObject attackButton;
    public GameObject pauseButton;

    // Update is called once per frame
    void Update()
    {
        if (Application.isMobilePlatform)
        {
            joystick.SetActive(true);
            jumpButton.SetActive(true);
            attackButton.SetActive(true);
            pauseButton.SetActive(true);
        }
    }
}
