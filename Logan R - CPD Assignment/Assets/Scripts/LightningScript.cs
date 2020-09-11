using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningScript : MonoBehaviour
{
    public ParticleSystem[] lightningParticles;

    private float timer = 0;
    private float waitTime = 3;

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer >= waitTime)
        {
            for (int i = 0; i < lightningParticles.Length; i++)
            {
                StartCoroutine(ShootLightning(i));
            }
            GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = true;
        }
    }

    IEnumerator ShootLightning(int index)
    {
        lightningParticles[index].gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        lightningParticles[index].gameObject.SetActive(true);
        timer = 0;
    }
}
