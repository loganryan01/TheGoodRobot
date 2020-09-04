using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public ParticleSystem smokeParticles;

    public void OnDestroy()
    {
        Instantiate(smokeParticles, transform.position, smokeParticles.transform.rotation);
        smokeParticles.Play();
    }
}
