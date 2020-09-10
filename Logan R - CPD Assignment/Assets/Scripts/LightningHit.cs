using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningHit : MonoBehaviour
{
    public GameObject player;

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!player.GetComponent<PlayerController>().isDead)
            {
                player.GetComponent<PlayerController>().isDead = true;
            }
        }
    }
}
