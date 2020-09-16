/*-------------------------------------------------------------------
    File name: Checkpoint.cs
    Purpose: Respawn the player at their location if they touched it.
    Author: Logan Ryan
    Modified: 16 September 2020
---------------------------------------------------------------------
    Copyright 2020 Logan Ryan
-------------------------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public bool activated = false;
    public static GameObject[] CheckPointsList;

    public MeshRenderer rend;

    public Material[] materials;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Number of checkpoints in the hierarchy
        int numCheckPoints = FindObjectsOfType<Checkpoint>().Length;

        // If there are more than 2 checkpoints
        if (numCheckPoints != 2)
        {
            // Destroy the game object
            Destroy(gameObject);
            
        }
        // Otherwise, make the object not to be destroyed on load
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CheckPointsList = GameObject.FindGameObjectsWithTag("CheckPoint");
        rend = gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is no longer on the game scene
        if (SceneManager.GetActiveScene().name != "My Game")
        {
            // Destroy 
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateCheckPoint();
        }
    }

    // Activate the checkpoint if the player touches it
    private void ActivateCheckPoint()
    {
        // Deactivate all checkpoints in the scene
        foreach (GameObject cp in CheckPointsList)
        {
            cp.GetComponent<Checkpoint>().activated = false;
            cp.GetComponent<MeshRenderer>().material = materials[0];
        }

        // We activate the current checkpoint
        activated = true;
        rend.material = materials[1];
    }

    // Get position of the last activated checkpoint
    public static Vector3 GetActiveCheckPointPosition()
    {
        // If player die without activate any checkpoint, we will return a default position
        Vector3 result = new Vector3(0, 0, 0);

        if (CheckPointsList != null)
        {
            foreach (GameObject cp in CheckPointsList)
            {
                // We search the activated checkpoint to get its position
                if (cp.GetComponent<Checkpoint>().activated)
                {
                    result = cp.transform.position;
                    break;
                }
            }
        }

        return result;
    }
}
