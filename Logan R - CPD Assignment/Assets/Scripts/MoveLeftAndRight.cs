using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeftAndRight : MonoBehaviour
{
    public float xRange = 5;
    private Vector3 direction = Vector3.right;
    public float speed = 10.0f;

    public bool applyWaitTime = false;
    private float waitTime = 3.0f;
    private float timer = 0.0f;
    private float numberOfChanges = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (applyWaitTime)
        {
            if (numberOfChanges != 4)
            {
                transform.Translate(direction * speed * Time.deltaTime);

                if (transform.position.x > xRange)
                {
                    numberOfChanges++;
                    direction = -Vector3.right;
                }
                else if (transform.position.x < -xRange)
                {
                    numberOfChanges++;
                    direction = Vector3.right;
                }

                Debug.Log(numberOfChanges);
            }
            else
            {
                timer += 1 * Time.deltaTime;
                if (timer >= waitTime)
                {
                    numberOfChanges = 0;
                    timer = 0;
                }
            }
            
        }
        else if (!applyWaitTime)
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
}
