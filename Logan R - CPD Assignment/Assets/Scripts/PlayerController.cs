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
    private DroppingPlatformScript droppingPlatform;
    public AudioClip deathSound;

    private Vector3 velocity;

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
    private bool played;

    // Start is called before the first frame update
    void Start()
    {
        robo3CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstraintPlayerPosition();
        scoreText.text = "Coins: " + playerCoins;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ----- Box Collision -----
        // Jumping on a box
        if (other.gameObject.CompareTag("Box") && falling)
        {
            playerCoins++;
            Destroy(other.gameObject);
            velocity.y = 0;
            velocity.y += 5.0f;
        }

        // ----- Coin Collision -----
        // Touching coin
        if (other.gameObject.CompareTag("Coin"))
        {
            playerCoins++;
            Destroy(other.gameObject);
        }

        // ----- Lightning Collision -----
        if (other.gameObject.CompareTag("Lightning"))
        {
            isDead = true;
        }

        if (other.gameObject.CompareTag("Respawn"))
        {
            isGrounded = true;
            velocity.y = 0;
        }

        // ----- Finish Collision -----
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        // ----- Moving Platform Collision -----
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = true;
            transform.parent = other.transform;
        }

        // ----- Dropping Platform Collision -----
        if (other.gameObject.CompareTag("DroppingPlatform"))
        {
            isGrounded = true;
            droppingPlatform = other.gameObject.GetComponent<DroppingPlatformScript>();
            droppingPlatform.playerOnPlatform = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // ----- Ground Collision -----
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (other.gameObject.CompareTag("Respawn"))
        {
            isGrounded = true;
        }

        // ----- Box Collision -----
        // If the player is not jumping, falling, or not on the ground
        if (other.gameObject.CompareTag("Box") && !jumping && !falling && !isGrounded)
        {
            isOnBox = true;
        }

        // ----- Box Collision -----
        if (other.gameObject.CompareTag("Box") && isAttacking)
        {
            playerCoins++;
            Destroy(other.gameObject);
            isOnBox = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // ----- Ground Collision -----
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }

        if (other.gameObject.CompareTag("Respawn"))
        {
            isGrounded = false;
        }

        // If the player is not jumping
        if (other.gameObject.CompareTag("Box"))
        {
            isOnBox = false;
        }

        // ----- Moving Platform Collision -----
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
            transform.parent = null;
        }

        // ----- Dropping Platform Collision -----
        if (other.gameObject.CompareTag("DroppingPlatform"))
        {
            isGrounded = false;
            droppingPlatform = null;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // ----- Enemy Collision -----
        // Jumping on enemy
        if (hit.gameObject.CompareTag("Enemy"))
        {
            enemyScript = hit.gameObject.GetComponent<EnemyScript>();

            if (!enemyScript.isDead && !isGrounded)
            {
                velocity.y = 0;
                velocity.y = 5;
                enemyScript.isDead = true;
            }
            else if (!enemyScript.isDead && isAttacking)
            {
                enemyScript.isDead = true;
            }
            else if (!enemyScript.isDead && !isAttacking && isGrounded)
            {
                isDead = true;
            }

            enemyScript = null;
        }
    }

    // Move the player through the arrow keys and jump with the spacebar
    void MovePlayer()
    {
        if (Application.isMobilePlatform)
        {
            if (!isDead)
            {
                // Moving Forward
                if (joystick.Vertical != 0 || joystick.Horizontal != 0)
                {
                    animator.SetBool("Run", true);

                }
                else
                {
                    animator.SetBool("Run", false);
                }

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
                string fireAxis = "Fire3";

                if (horizontalInput != 0 || verticalInput != 0)
                {
                    animator.SetBool("Run", true);
                }
                else
                {
                    animator.SetBool("Run", false);
                }

                Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
                robo3CharacterController.Move(move * speed * Time.deltaTime);

                if (move != Vector3.zero && Time.timeScale != 0.0f)
                {
                    gameObject.transform.forward = move;
                }

                if (Input.GetButtonDown(fireAxis))
                {
                    Attack();
                }

                if (Input.GetButtonDown(jumpAxis))
                {
                    Jump();
                }
            }
        }

        if (!isDead)
        {
            velocity.y += gravity * Time.deltaTime;
            
            if (isGrounded && velocity.y < 0 ||
                isOnBox && velocity.y < 0)
            {
                velocity.y = 0;
            }

            if (!isGrounded && velocity.y < 0 ||
                !isOnBox && velocity.y < 0)
            {
                falling = true;
            }
            else
            {
                falling = false;
            }

            if (!isGrounded && velocity.y > 0 ||
                !isOnBox && velocity.y > 0)
            {
                jumping = true;
            }
            else
            {
                jumping = false;
            }

            robo3CharacterController.Move(velocity * Time.deltaTime);

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
            
            animator.SetBool("Dead", true);

            if (!played)
            {
                AudioSource.PlayClipAtPoint(deathSound, transform.position);
                played = true;
            }
        }
    }

    // Attack function
    public void Attack()
    {
        if (!isAttacking)
        {
            animator.Play("Attack");
        }
    }

    // Jump function
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
        if (transform.position.z < 5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 5f);
        }
    }
}
