﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    public ParticleSystem lightningParticles;

    private float timer = 0;
    private float waitTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1 * Time.deltaTime;

        if (timer >= waitTime)
        {
            StartCoroutine(ShootLightning());
        }
    }

    IEnumerator ShootLightning()
    {
        lightningParticles.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        lightningParticles.gameObject.SetActive(true);
        timer = 0;
    }
}