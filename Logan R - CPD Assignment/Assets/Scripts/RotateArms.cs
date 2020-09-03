using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateArms : MonoBehaviour
{
    public bool rotateArmsUp = false;
    public bool rotateArmsDown = false;
    private float rotationSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float fireInput = Input.GetAxis("Fire3");
        
        if (fireInput == 1 && transform.rotation.x >= 0.0f)
        {
            rotateArmsUp = true;
        }

        // Rotate arms up
        if (rotateArmsUp)
        {
            transform.Rotate(-Vector3.right * rotationSpeed * Time.deltaTime);

            if (transform.rotation.x < -0.7f)
            {
                rotateArmsUp = false;
                rotateArmsDown = true;
            }
        }

        // Rotate arms down
        if (rotateArmsDown)
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);

            if (transform.rotation.x > 0.0f)
            {
                rotateArmsDown = false;
            }
        }
    }
}
