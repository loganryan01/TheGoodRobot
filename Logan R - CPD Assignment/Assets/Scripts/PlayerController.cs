using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Vector3 startPos;
    public Animator animator;
    public Text scoreText;

    public float speed = 10.0f;
    public float jumpForce;
    public float gravityModifier;
    public float maximumRotation = 90.0f;
    public float playerCoins = 0;

    public bool isOnGround;
    public bool isAttacking;
    public bool turning = false;
    
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
        scoreText.text = "Coins: " + playerCoins;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

        // Check if player jumps on enemy or box
        if (collision.gameObject.CompareTag("Enemy") && !isOnGround ||
            collision.gameObject.CompareTag("Box") && !isOnGround ||
            collision.gameObject.CompareTag("Coin"))
        {
            if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Coin"))
            {
                playerCoins++;
            }
            
            if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<PlayParticles>().smokeParticles.Play();
            }
            
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") && isOnGround && !isAttacking ||
            collision.gameObject.CompareTag("Lightning"))
        {
            transform.position = startPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Box"))
        {
            playerCoins++;
        }

        // Check if player attacks enemy or box
        if (other.gameObject.CompareTag("Enemy") && isAttacking ||
            other.gameObject.CompareTag("Box") && isAttacking)
        {
            other.gameObject.GetComponent<PlayParticles>().smokeParticles.Play();
            Destroy(other.gameObject);
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
        }

        if (verticalInput > 0 && !turning)
        {
            if (transform.eulerAngles.y != Vector3.zero.y)
            {
                turning = true;
                StartCoroutine(LerpFunction(Quaternion.Euler(Vector3.zero), 1));
            }
        }
        else if (verticalInput < 0 && !turning)
        {
            if (transform.eulerAngles.y != (maximumRotation * 2.0f))
            {
                turning = true;
                StartCoroutine(LerpFunction(Quaternion.Euler(0, (maximumRotation * 2.0f), 0), 1));
            }
        }
        else if (horizontalInput < 0 && !turning)
        {
            if (transform.eulerAngles.y != (maximumRotation * 3.0f))
            {
                turning = true;
                StartCoroutine(LerpFunction(Quaternion.Euler(0, (maximumRotation * 3.0f), 0), 1));
            }
        }
        else if (horizontalInput > 0 && !turning)
        {
            if (transform.eulerAngles.y != maximumRotation)
            {
                turning = true;
                StartCoroutine(LerpFunction(Quaternion.Euler(0, maximumRotation, 0), 1));
            }
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
        turning = false;
    }
}
