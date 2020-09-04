using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public GameObject player;
    private Vector3 respawnPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "Player")
        {
            player.transform.position = respawnPosition;
        }
    }
}
