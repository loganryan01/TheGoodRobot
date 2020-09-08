using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatformScript : MonoBehaviour
{
    public bool playerOnPlatform;
    private bool drop;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (drop)
        {
            transform.Translate(-Vector3.up * 10.0f * Time.deltaTime);
        }

        if (playerOnPlatform)
        {
            StartCoroutine(Drop());
        }
    }

    IEnumerator Drop()
    {
        playerOnPlatform = false;
        // Wait 5 seconds then drop
        yield return new WaitForSeconds(1);
        drop = true;
    }
}
