using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround;
    
    // Start is called before the first frame update
    void Start()
    {
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

        if (collision.gameObject.CompareTag("Enemy") && !isOnGround ||
            collision.gameObject.CompareTag("Box") && !isOnGround)
        {
            Destroy(collision.gameObject);
        }
    }

    // Move the player through the arrow keys and jump with the spacebar
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Move through Rigidbody
        //playerRb.AddForce(Vector3.right * speed * horizontalInput);
        //playerRb.AddForce(Vector3.forward * speed * verticalInput);

        // Move through translate
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
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
}
