using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHit : MonoBehaviour
{
    public GameObject player;

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "Player")
        {
            player.GetComponent<PlayerController>().isDead = true;
        }
    }
}
