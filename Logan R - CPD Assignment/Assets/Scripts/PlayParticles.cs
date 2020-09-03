using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticles : MonoBehaviour
{
    public ParticleSystem smokeParticles;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        smokeParticles.transform.position = transform.position;
    }

    public void OnDestroy()
    {
        smokeParticles.Play();
    }
}
