using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo3Script : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 8.0f;
    public float maximumRotation = 90.0f;
    public float gravity = 20.0f;

    public Animator robo3Controller;
    public CharacterController robo3CharacterController;
    private Vector3 moveDirection = Vector3.zero;

    public bool turning = false;
    public bool isAttacking;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        robo3CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float jumpInput = Input.GetAxis("Jump");
        float fireInput = Input.GetAxis("Fire3");

        robo3CharacterController.Move(Vector3.right * horizontalInput * speed * Time.deltaTime);
        robo3CharacterController.Move(Vector3.forward * verticalInput * speed * Time.deltaTime);

        if (horizontalInput != 0 || verticalInput != 0)
        {
            robo3Controller.SetBool("Run", true);
        }
        else
        {
            robo3Controller.SetBool("Run", false);
        }

        if (isGrounded && jumpInput == 1)
        {
            moveDirection.y = jumpForce;
            isGrounded = false;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        robo3CharacterController.Move(moveDirection * Time.deltaTime);

        if (fireInput == 1)
        {
            robo3Controller.SetBool("Attack", true);
            isAttacking = true;
        }

        if (robo3Controller.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            robo3Controller.SetBool("Attack", false);
            isAttacking = false;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy") && isAttacking)
        {
            Destroy(hit.gameObject);
        }

        if (hit.gameObject.CompareTag("Enemy") && !isGrounded)
        {
            moveDirection.y = jumpForce;
            Destroy(hit.gameObject);
        }

        if (hit.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
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
        turning = false;
    }
}
