using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHit : MonoBehaviour
{
    public GameObject player;
    private Vector3 respawnPosition;

    private void Start()
    {
        respawnPosition = player.transform.position;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "Player")
        {
            player.transform.position = respawnPosition;
        }
    }
}
