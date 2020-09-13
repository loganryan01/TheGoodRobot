using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public AudioClip collectedSoundEffect;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 1.0f, Space.World);
    }

    private void OnDestroy()
    {
        AudioSource.PlayClipAtPoint(collectedSoundEffect, transform.position);
    }
}
