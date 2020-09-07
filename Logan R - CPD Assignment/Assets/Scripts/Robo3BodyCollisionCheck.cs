using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo3BodyCollisionCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Touching Enemy");
            //Destroy(collision.gameObject);
        }
    }
}
