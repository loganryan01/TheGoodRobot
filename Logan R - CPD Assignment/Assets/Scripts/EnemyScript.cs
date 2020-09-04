using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    public ParticleSystem smokeParticles;

    public float xRange = 5;
    public float speed = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x > xRange)
        {
            direction = -Vector3.right;
        }
        else if (transform.position.x < -xRange)
        {
            direction = Vector3.right;
        }
    }

    public void OnDestroy()
    {
        Instantiate(smokeParticles, transform.position, smokeParticles.transform.rotation);
        smokeParticles.Play();
    }
}
