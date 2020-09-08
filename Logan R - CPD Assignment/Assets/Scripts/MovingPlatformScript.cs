using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public float speed = 10.0f;
    public float xRange = 0;
    public float zRange = 0;

    public Vector3 xDirection = Vector3.right;
    public Vector3 zDirection = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (xRange != 0)
        {
            if (transform.position.x > xRange)
            {
                xDirection = -Vector3.right;
            }
            else if (transform.position.x < -xRange)
            {
                xDirection = Vector3.right;
            }

            transform.Translate(xDirection * speed * Time.deltaTime);
        }

        if (zRange != 0)
        {
            if (transform.position.z > zRange)
            {
                zDirection = -Vector3.forward;
            }
            else if (transform.position.z < -zRange)
            {
                zDirection = Vector3.forward;
            }

            transform.Translate(zDirection * speed * Time.deltaTime);
        }
    }
}
