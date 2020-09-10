using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Text scoreText;
    public Joystick joystick;
    public CharacterController robo3CharacterController;
    public EnemyScript enemyScript;
    private DroppingPlatformScript droppingPlatform;
    private GameObject box;

    //private Vector3 startPos;
    private Vector3 velocity;

    public float speed = 10.0f;
    public float jumpForce = 1.0f;
    public float gravity = -9.81f;
    public float maximumRotation = 90.0f;
    public float playerCoins = 0;

    public bool isGrounded;
    public bool isAttacking;
    public bool turning = false;
    public bool onPlatform;
    public bool touchingEnemy;
    public bool isDead;
    public bool touchingBox;

    // Start is called before the first frame update
    void Start()
    {
        //startPos = transform.position;
        robo3CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        ConstraintPlayerPosition();
        scoreText.text = "Coins: " + playerCoins;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ----- Ground Collision -----
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // ----- Coin Collision -----
        // Touching coin
        if (other.gameObject.CompareTag("Coin"))
        {
            playerCoins++;
            Destroy(other.gameObject);
        }

        // ----- Box Collision -----
        // Jumping on box
        if (other.gameObject.CompareTag("Box") && !isGrounded)
        {
            playerCoins++;
            Destroy(other.gameObject);
            velocity.y = jumpForce;
        }

        // ----- Lightning Collision -----
        if (other.gameObject.CompareTag("Lightning"))
        {
            isDead = true;
        }

        // ----- Finish Collision -----
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Box") && isAttacking)
        {
            playerCoins++;
            Destroy(other.gameObject);
            touchingBox = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // ----- Enemy Collision -----
        // Touching enemy
        if (hit.gameObject.CompareTag("Enemy"))
        {
            enemyScript = hit.gameObject.GetComponent<EnemyScript>();

            if (!enemyScript.isDead)
            {
                touchingEnemy = true;
            }
        }

        // Jumping on enemy
        if (hit.gameObject.CompareTag("Enemy") && !isGrounded)
        {
            enemyScript = hit.gameObject.GetComponent<EnemyScript>();

            if (!enemyScript.isDead)
            {
                velocity.y = jumpForce;
            }

            enemyScript.isDead = true;
            touchingEnemy = false;
        }

        //// ----- Moving Platform Collision -----
        //// Touching moving platform
        //if (other.gameObject.CompareTag("MovingPlatform"))
        //{
        //    isGrounded = true;
        //    transform.parent = hit.transform;
        //}
        //else
        //{
        //    transform.parent = null;
        //}

        //// ----- Dropping Platform Collision -----
        //// Touching dropping platform
        //if (hit.gameObject.CompareTag("DroppingPlatform"))
        //{
        //    droppingPlatform = hit.gameObject.GetComponent<DroppingPlatformScript>();
        //    droppingPlatform.playerOnPlatform = true;
        //}
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
                robo3CharacterController.Move(move * speed * Time.fixedDeltaTime);

                if (move != Vector3.zero)
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
                float jumpInput = Input.GetAxis("Jump");
                float fireInput = Input.GetAxis("Fire3");

                if (isGrounded && velocity.y < 0)
                {
                    velocity.y = 0;
                }

                if (horizontalInput != 0 || verticalInput != 0)
                {
                    animator.SetBool("Run", true);
                }
                else
                {
                    animator.SetBool("Run", false);
                }

                Vector3 move = new Vector3(horizontalInput, 0, verticalInput);
                robo3CharacterController.Move(move * speed * Time.fixedDeltaTime);

                if (move != Vector3.zero)
                {
                    gameObject.transform.forward = move;
                }

                if (fireInput == 1)
                {
                    Attack();
                }

                if (jumpInput == 1)
                {
                    Jump();
                }
            }
        }

        if (!isDead)
        {
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = 0;
            }

            if (!isGrounded)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            robo3CharacterController.Move(velocity * Time.fixedDeltaTime);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttacking = true;
            }
            else if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttacking = false;
            }

            // ----- Collision Check -----
            // Attacking enemy
            if (touchingEnemy && isAttacking)
            {
                enemyScript.isDead = true;
                enemyScript = null;
                touchingEnemy = false;
            }
            else if (touchingEnemy && !enemyScript.isDead)
            {
                isDead = true;
            }

            // Touching nothing
            //if (robo3CharacterController.collisionFlags == CollisionFlags.None)
            //{
            //    isGrounded = false;
            //    transform.parent = null;
            //}
        }

        if (isDead)
        {
            animator.SetBool("Dead", true);
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
        if (isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpForce * -3.0f * gravity);
            isGrounded = false;
        }
    }

    // Prevent the player from going out of bounds
    void ConstraintPlayerPosition()
    {
        if (transform.position.z < -0.5)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.5f);
        }
    }
}
