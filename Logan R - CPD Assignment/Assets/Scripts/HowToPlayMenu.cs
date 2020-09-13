using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HowToPlayMenu : MonoBehaviour
{
    private string[] controllers;

    public GameObject mainMenu;
    public GameObject startButton;
    public GameObject keyboardControls;
    public GameObject joystickControls;
    public GameObject player;
    public GameObject stand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controllers = Input.GetJoystickNames();
        Debug.Log(controllers.Length);

        if (controllers.Length > 0)
        {
            keyboardControls.SetActive(false);
            joystickControls.SetActive(true);
        }
        else
        {
            keyboardControls.SetActive(true);
            joystickControls.SetActive(false);
        }
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        player.SetActive(true);
        stand.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(startButton);
        gameObject.SetActive(false);
    }
}
