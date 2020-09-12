using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Vector3 direction = Vector3.right;
    private Vector3 startPos;

    public ParticleSystem smokeParticles;
    public CharacterController robo2Controller;
    public Animator robo2Animator;
    public PlayerController playerController;

    public float xRange = 5;
    public float speed = 10.0f;
    private float faceRight = 90;
    private float faceLeft = 270;

    public bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        robo2Controller = GetComponent<CharacterController>();
        robo2Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            StartCoroutine(PlayDead());
            robo2Controller.enabled = false;
        }
        else
        {
            robo2Controller.Move(direction * speed * Time.deltaTime);

            if (transform.position.x > (startPos.x + xRange))
            {
                // Stop moving and turn
                direction = Vector3.zero;
                robo2Animator.SetBool("AtEdge", true);
                StartCoroutine(LerpFunction(Quaternion.Euler(0, faceLeft, 0), 1.0f));

                if (transform.eulerAngles.y == faceLeft)
                {
                    robo2Animator.SetBool("AtEdge", false);
                    direction = -Vector3.right;
                }
            }
            else if (transform.position.x < (startPos.x - xRange))
            {
                direction = Vector3.zero;
                robo2Animator.SetBool("AtEdge", true);
                StartCoroutine(LerpFunction(Quaternion.Euler(0, faceRight, 0), 1.0f));

                if (transform.eulerAngles.y == faceRight)
                {
                    robo2Animator.SetBool("AtEdge", false);
                    direction = Vector3.right;
                }
            }
        }

        transform.position = new Vector3(transform.position.x, 0, startPos.z);
    }

    public void OnDestroy()
    {
        Instantiate(smokeParticles, transform.position, smokeParticles.transform.rotation);
        smokeParticles.Play();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            playerController = hit.gameObject.GetComponent<PlayerController>();

            if (!playerController.isDead && !playerController.isAttacking && playerController.isGrounded)
            {
                playerController.isDead = true;
            }

            if (!playerController.isDead && playerController.isAttacking)
            {
                isDead = true;
            }
        }

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

    IEnumerator PlayDead()
    {
        robo2Animator.SetBool("Dead", true);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
