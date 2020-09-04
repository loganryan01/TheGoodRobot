using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndRight : MonoBehaviour
{
    public float xRange = 5;
    private Vector3 direction = Vector3.right;
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

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
}
