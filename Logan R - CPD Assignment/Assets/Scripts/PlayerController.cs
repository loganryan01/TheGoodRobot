using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Vector3 startPos;
    public Animator animator;
    private GameObject leftArm;

    public float speed = 10.0f;
    public float jumpForce;
    public float gravityModifier;

    public bool isOnGround;
    public bool isAttacking;
    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ConstraintPlayerPosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        // Check if player jumps on enemy or box
        if (collision.gameObject.CompareTag("Enemy") && !isOnGround ||
            collision.gameObject.CompareTag("Box") && !isOnGround)
        {
            collision.gameObject.GetComponent<PlayParticles>().smokeParticles.Play();
            Destroy(collision.gameObject);
        }

        // Check if player attacks enemy or box
        if (collision.gameObject.CompareTag("Enemy") && isAttacking ||
            collision.gameObject.CompareTag("Box") && isAttacking)
        {
            Debug.Log("There is a collision with the arms");
            collision.gameObject.GetComponent<PlayParticles>().smokeParticles.Play();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") && isOnGround ||
            collision.gameObject.CompareTag("Lightning"))
        {
            transform.position = startPos;
        }
    }

    // Move the player through the arrow keys and jump with the spacebar
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float jumpInput = Input.GetAxis("Jump");
        float fireInput = Input.GetAxis("Fire3");

        if (fireInput == 1)
        {
            StartCoroutine(RotatePlayerArms());
            //animator.Play("Attack_Start");
        }

        // If the player is rotatiing to the right stop them from going greater than 90
        if (horizontalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else if (horizontalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 270, 0);
        }

        if (verticalInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (verticalInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        

        // Move through translate
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);

        if (jumpInput == 1 && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
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

    IEnumerator RotatePlayerArms()
    {
        isAttacking = true;
        animator.Play("Attack_Start");

        yield return new WaitForSeconds(1.0f);

        isAttacking = false;
    }
}
