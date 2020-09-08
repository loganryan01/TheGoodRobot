using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPlatformScript : MonoBehaviour
{
    private bool startDrop;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startDrop)
        {
            transform.Translate(-Vector3.up * 10.0f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("There is a collision");
        
        if (!startDrop)
        {
            StartCoroutine(Drop());
        }
        
    }

    IEnumerator Drop()
    {
        // Wait 5 seconds then drop
        yield return new WaitForSeconds(5);
        startDrop = true;
    }
}
