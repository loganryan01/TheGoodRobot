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

    private void Awake()
    {
        int numCheckPoints = FindObjectsOfType<Checkpoint>().Length;

        if (numCheckPoints != 2)
        {
            Destroy(gameObject);
            
        }
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
        if (SceneManager.GetActiveScene().name != "My Game")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Touching Player");
            ActivateCheckPoint();
        }
    }

    private void ActivateCheckPoint()
    {
        // We deactive all checkpoints in the scene
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
