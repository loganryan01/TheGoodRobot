/*-----------------------------------------
    File name: EnemyScript.cs
    Purpose: Control the enemy's behaviour.
    Author: Logan Ryan
    Modified: 15 September 2020
-------------------------------------------
    Copyright 2020 Logan Ryan
-----------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public ParticleSystem smokeParticles;
    public AudioClip deathSound;

    public float xRange = 5;
    public float speed = 10.0f;

    public bool isDead;

    private CharacterController robo2Controller;
    private Animator robo2Animator;
    private PlayerController playerController;

    private Vector3 movingDirection = Vector3.right;
    private Vector3 startPos;

    private float faceRight = 90;
    private float faceLeft = 270;

    // Start is called before the first frame update
    void Start()
    {
        // Get the starting position
        startPos = transform.position;

        // Get the enemy's character controller
        robo2Controller = GetComponent<CharacterController>();

        // Get the enemy's animator
        robo2Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // If the enemy should be dead
        if (isDead)
        {
            // Start the PlayDead coroutine
            StartCoroutine(PlayDead());

            // Disable the character controller
            robo2Controller.enabled = false;
        }
        // If the enemy should move
        else if (xRange != 0)
        {
            // Move in the direction at a certain speed
            robo2Controller.Move(movingDirection * speed * Time.deltaTime);

            // If the enemy is at the right edge of its patrol path
            if (transform.position.x > (startPos.x + xRange))
            {
                // Stop moving
                movingDirection = Vector3.zero;

                // Set the animator boolean "AtEdge" to true, to play the idle animation
                robo2Animator.SetBool("AtEdge", true);

                // Rotate to face the left side
                StartCoroutine(LerpFunction(Quaternion.Euler(0, faceLeft, 0), 1.0f));

                // Once the enemy is facing left
                if (transform.eulerAngles.y == faceLeft)
                {
                    // Set the animator boolean "AtEdge" to false, to play the run animation 
                    robo2Animator.SetBool("AtEdge", false);

                    // Set the moving direction to be left
                    movingDirection = -Vector3.right;
                }
            }
            // If the enemy is at the left edge of its patrol path
            else if (transform.position.x < (startPos.x - xRange))
            {
                // Repeat the same process above but this time enemy should be facing right

                movingDirection = Vector3.zero;
                robo2Animator.SetBool("AtEdge", true);
                StartCoroutine(LerpFunction(Quaternion.Euler(0, faceRight, 0), 1.0f));

                if (transform.eulerAngles.y == faceRight)
                {
                    robo2Animator.SetBool("AtEdge", false);
                    movingDirection = Vector3.right;
                }
            }
        }
        else
        {
            // If the robot should not be moving at all, then just play the idle animation
            robo2Animator.SetBool("AtEdge", true);
        }

        // Prevent enemy from moving up, down, forward and backward
        transform.position = new Vector3(transform.position.x, startPos.y, startPos.z);
    }

    // OnDestory is called when the MonoBehaviour will be destroyed
    public void OnDestroy()
    {
        // Play the death sound effect at the enemy's current position
        AudioSource.PlayClipAtPoint(deathSound, transform.position);

        // Add smoke particles to the enemy object
        Instantiate(smokeParticles, transform.position, smokeParticles.transform.rotation);

        // Play the smoke particles
        smokeParticles.Play();
    }

    // OnControllerColliderHit is called when the MonoBehaviour hits a collider while performing a Move
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If the enemy touches the player
        if (hit.gameObject.CompareTag("Player"))
        {
            // Get the player controller script
            playerController = hit.gameObject.GetComponent<PlayerController>();

            // If the player is not dead, not attacking as is on the ground
            if (!playerController.isDead && !playerController.isAttacking && playerController.isGrounded)
            {
                // The player is now dead
                playerController.isDead = true;
            }

            // If the player is not dead and is attacking
            if (!playerController.isDead && playerController.isAttacking)
            {
                // The enemy is now dead
                isDead = true;
            }
        }

    }

    // LerpFunction is a coroutine to rotate the object
    IEnumerator LerpFunction(Quaternion endValue, float duration)
    {
        // Time it has taken to rotate
        float time = 0;

        // Starting rotation
        Quaternion startValue = transform.rotation;

        // While the time is less than the duration (How long it should take to rotate)
        while (time < duration)
        {
            // Rotate from the starting value
            transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);

            // Add the delta time to the time variable
            time += Time.deltaTime;
            yield return null;
        }

        // Set the rotation to the endValue when the time is up 
        transform.rotation = endValue;
    }

    // PlayDead is a coroutine to play the dead animation and destroy the enemy object
    IEnumerator PlayDead()
    {
        robo2Animator.SetBool("Dead", true);
        yield return new WaitForSeconds(robo2Animator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(gameObject);
    }
}
