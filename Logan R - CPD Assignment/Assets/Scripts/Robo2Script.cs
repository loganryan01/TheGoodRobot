using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo2Script : MonoBehaviour
{
    private Vector3 direction = Vector3.right;

    public ParticleSystem smokeParticles;
    public CharacterController robo2Controller;
    public Animator robo2Animator;

    public float xRange = 5;
    public float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        robo2Controller = GetComponent<CharacterController>();
        robo2Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        robo2Controller.Move(direction * speed * Time.deltaTime);

        if (transform.position.x > xRange)
        {
            // Stop moving and turn
            direction = Vector3.zero;
            robo2Animator.SetBool("RightEdge", true);
            StartCoroutine(LerpFunction(Quaternion.Euler(0, 270.0f, 0), 1.0f));

            if (transform.eulerAngles.y == 270.0f)
            {
                robo2Animator.SetBool("RightEdge", false);
                direction = -Vector3.right;
            }
        }
        else if (transform.position.x < -xRange)
        {
            direction = Vector3.zero;
            robo2Animator.SetBool("RightEdge", true);
            StartCoroutine(LerpFunction(Quaternion.Euler(0, 90.0f, 0), 1.0f));
            

            if (transform.eulerAngles.y == 90.0f)
            {
                robo2Animator.SetBool("RightEdge", false);
                direction = Vector3.right;
            }
        }
    }

    public void OnDestroy()
    {
        Instantiate(smokeParticles, transform.position, smokeParticles.transform.rotation);
        smokeParticles.Play();
    }

    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = transform.rotation;

        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endValue;
    }
}
