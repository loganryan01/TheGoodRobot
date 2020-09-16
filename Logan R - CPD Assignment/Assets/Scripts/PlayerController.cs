/*-----------------------------------------------------
    File name: PlayerController.cs
    Purpose: Control the player characeter in the game.
    Author: Logan Ryan
    Modified: 16 September 2020
-------------------------------------------------------
    Copyright 2020 Logan Ryan
-----------------------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI scoreText;
    public Joystick joystick;
    public CharacterController robo3CharacterController;
    public EnemyScript enemyScript;
    public AudioClip deathSound;

    public float speed = 10.0f;
    public float jumpForce = 1.0f;
    public float gravity = -9.81f;
    public float playerCoins = 0;

    public bool isGrounded;
    public bool isAttacking;
    public bool isOnBox;
    public bool onPlatform;
    public bool isDead;
    public bool touchingBox;
    public bool falling;
    public bool jumping;

    private DroppingPlatformScript droppingPlatform;

    private Vector3 velocity;

    private float zBound = 5f;

    private bool played;

    // Start is called before the first frame update
    void Start()
    {
        robo3CharacterController = GetComponent<CharacterController>();

        if (Checkpoint.GetActiveCheckPointPosition() != Vector3.zero)
        {
            Debug.Log("Spawning at checkpoint");
            robo3CharacterController.transform.position = Checkpoint.GetActiveCheckPointPosition();
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstraintPlayerPosition();
        scoreText.text = "Coins: " + playerCoins;
    }

    /// <summary>
    /// OnTriggerEnter is called when the collider other enters the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // ----- Box Collision -----
        // If the player jumps on the box
        if (other.gameObject.CompareTag("Box") && falling)
        {
            // Increase score by 1
            playerCoins++;

            // Destroy box
            Destroy(other.gameObject);

            // Make player bounce up
            velocity.y = 0;
            velocity.y += 8.0f;
        }

        // ----- Coin Collision -----
        // When player collects a coin
        if (other.gameObject.CompareTag("Coin"))
        {
            // Increase score by 1 and destroy coin
            playerCoins++;
            Destroy(other.gameObject);
        }

        // ----- Lightning Collision -----
        // When the player touches the lightning of an electric fence
        if (other.gameObject.CompareTag("Lightning"))
        {
            // The player is dead
            isDead = true;
        }

        // ---- Starting teleporter -----
        // If the player is on top of the starting teleporter, player can then jump if they wanted to
        if (other.gameObject.CompareTag("Respawn"))
        {
            isGrounded = true;
            velocity.y = 0;
        }

        // ----- Finish Collision -----
        // If the player touches the finishing teleporter, go to the victory screen
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // ----- Moving Platform Collision -----
        // If the player is on a moving platform,
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            // Make the player a child of the moving platform, to allow them to move with the platform
            // and allow them to jump off the platform
            isGrounded = true;
            transform.parent = other.transform;
        }

        // ----- Dropping Platform Collision -----
        // If the player touches the dropping platform,
        if (other.gameObject.CompareTag("DroppingPlatform"))
        {
            // Start the timer for the platform.
            isGrounded = true;
            droppingPlatform = other.gameObject.GetComponent<DroppingPlatformScript>();
            droppingPlatform.playerOnPlatform = true;
        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other that is touching the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerStay(Collider other)
    {
        // ----- Ground Collision -----
        // If the player is touching the ground, then they are on the ground
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // ----- Starting Teleporter Collision -----
        // If the player is on top of the starting teleporter, player can then jump if they wanted to
        if (other.gameObject.CompareTag("Respawn"))
        {
            isGrounded = true;
            velocity.y = 0;
        }

        // ----- Box Collision -----
        // If the player is not jumping, falling, or not on the ground
        if (other.gameObject.CompareTag("Box") && !jumping && !falling && !isGrounded)
        {
            // Then they are just standing on top of the box
            isOnBox = true;
        }

        // If the player is attacking the box
        if (other.gameObject.CompareTag("Box") && isAttacking)
        {
            // Increase player score by 1
            playerCoins++;

            // Destroy the box
            Destroy(other.gameObject);
            isOnBox = false;
        }
    }

    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        // ----- Ground Collision -----
        // If the player is no longer touching the ground then they are in the air
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        // ----- Starting Teleporter Collision -----
        // If the player is no longer touching the starting teleporter then they are in the air
        if (other.gameObject.CompareTag("Respawn"))
        {
            isGrounded = false;
        }

        // ----- Box Collision -----
        // If the player is no longer touching the box then they are in the air
        if (other.gameObject.CompareTag("Box"))
        {
            isOnBox = false;
        }

        // ----- Moving Platform Collision -----
        // If the player is no longer touching a moving platform then they are in the air
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
            transform.parent = null;
        }

        // ----- Dropping Platform Collision -----
        // If the player is no longer touching the droping platform then they are in the air
        if (other.gameObject.CompareTag("DroppingPlatform"))
        {
            isGrounded = false;
            droppingPlatform = null;
        }
    }

    /// <summary>
    /// OnControllerColliderHit is called when the controller hits a collider while performing a Move
    /// </summary>
    /// <param name="hit"></param>
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // ----- Enemy Collision -----
        if (hit.gameObject.CompareTag("Enemy"))
        {
            // Get the enemy script of that enemy
            enemyScript = hit.gameObject.GetComponent<EnemyScript>();

            // If the player jumps on the enemy or 
            if (!enemyScript.isDead && !isGrounded)
            {
                // Player bounces up and destroy's the enemy
                velocity.y = 0;
                velocity.y = 5;
                enemyScript.isDead = true;
            }
            // If the player attacks the enemy
            else if (!enemyScript.isDead && isAttacking)
            {
                // Destroy the enemy
                enemyScript.isDead = true;
            }
            // If the player touches the robot without jumping or attacking
            else if (!enemyScript.isDead && !isAttacking && isGrounded)
            {
                // The player is dead
                isDead = true;
            }

            // Unload the enemy script
            enemyScript = null;
        }
    }

    // MovePlayer controls the movement of the player character
    void MovePlayer()
    {
        // If the player is playing on mobile
        if (Application.isMobilePlatform)
        {
            // If the player is not dead
            if (!isDead)
            {
                // Play the run animation if the joystick is moved
                if (joystick.Vertical != 0 || joystick.Horizontal != 0)
                {
                    animator.SetBool("Run", true);

                }
                else
                {
                    animator.SetBool("Run", false);
                }

                // Move in the direction of the joystick
                Vector3 move = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
                robo3CharacterController.Move(move * speed * Time.deltaTime);

                if (move != Vector3.zero && Time.timeScale != 0.0f)
                {
                    gameObject.transform.forward = move;
                }
            }
        }
        else
        {
            if (!isDead)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                string jumpAxis = "Jump";
                string fireAxis = "Fire2";

                // Play the run animation if the arrow keys or joystick is moved
                if (horizontalInput != 0 || verticalInput != 0)
                {
                    animator.SetBool("Run", true);
                }
                else
                {
                    animator.SetBool("Run", false);
                }

                // Move in that direction of the joystick
                Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
                robo3CharacterController.Move(move * speed * Time.deltaTime);

                if (move != Vector3.zero && Time.timeScale != 0.0f)
                {
                    gameObject.transform.forward = move;
                }

                // If the player presses the attack button
                if (Input.GetButtonDown(fireAxis))
                {
                    Attack();
                }

                // If the player presses the jump button
                if (Input.GetButtonDown(jumpAxis))
                {
                    Jump();
                }
            }
        }

        if (!isDead)
        {
            // Apply gravity to velocity of player
            velocity.y += gravity * Time.deltaTime;
            
            // If the player is on the ground or standing on a box do not apply gravity
            if (isGrounded && velocity.y < 0 ||
                isOnBox && velocity.y < 0)
            {
                velocity.y = 0;
            }

            // If the player is in the air and traveling downward on the y axis,
            // then the player is falling down
            if (!isGrounded && velocity.y < 0 ||
                !isOnBox && velocity.y < 0)
            {
                falling = true;
            }
            else
            {
                falling = false;
            }

            // If the player is in the air and traveling upward on the y axis,
            // then the player is jumping
            if (!isGrounded && velocity.y > 0 ||
                !isOnBox && velocity.y > 0)
            {
                jumping = true;
            }
            else
            {
                jumping = false;
            }

            // Apply velocity
            robo3CharacterController.Move(velocity * Time.deltaTime);

            // If the attack animation is playing, then the player is attacking
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttacking = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttacking = false;
            }

            // Check if player has fallen off
            if (transform.position.y < -5)
            {
                isDead = true;
            }

        }

        if (isDead)
        {
            // Play the death animation and death sound effect
            animator.SetBool("Dead", true);

            if (!played)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                played = true;
            }
        }
    }

    // Attack plays the attack animation of the player
    public void Attack()
    {
        if (!isAttacking)
        {
            // Play attack animation
            animator.Play("Attack");
        }
    }

    // Jump gives the player ability to jump
    public void Jump()
    {
        if (isGrounded || isOnBox)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
            isGrounded = false;
        }
    }

    // Prevent the player from going out of bounds
    void ConstraintPlayerPosition()
    {
        if (transform.position.z < zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
    }
}
