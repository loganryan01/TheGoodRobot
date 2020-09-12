using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public float speed = 10.0f;
    public float xRange = 0;
    public float zRange = 0;

    public Vector3 xDirection = Vector3.zero;
    public Vector3 zDirection = Vector3.zero;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (xRange != 0)
        {
            if (xDirection == Vector3.zero)
            {
                xDirection = Vector3.right;
            }
            
            if (transform.position.x > (startPos.x + xRange))
            {
                xDirection = -Vector3.right;
            }
            else if (transform.position.x < (startPos.x - xRange))
            {
                xDirection = Vector3.right;
            }

            transform.Translate(xDirection * speed * Time.fixedDeltaTime);
        }

        if (zRange != 0)
        {
            if (zDirection == Vector3.zero)
            {
                zDirection = Vector3.forward;
            }

            if (transform.position.z > (startPos.z + zRange))
            {
                zDirection = -Vector3.forward;
            }
            else if (transform.position.z < (startPos.z - zRange))
            {
                zDirection = Vector3.forward;
            }

            transform.Translate(zDirection * speed * Time.fixedDeltaTime);
        }
    }
}
