using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robo3Script : MonoBehaviour
{
    public float speed = 10.0f;
    public float jumpForce = 10.0f;
    public float maximumRotation = 90.0f;

    public Animator robo3Controller;
    public SkinnedMeshRenderer meshRenderer;
    public new MeshCollider collider;

    public bool isOnGround;
    public bool jumping;
    public bool turning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float jumpInput = Input.GetAxis("Jump");

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime, Space.World);
        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime, Space.World);
        

        if (horizontalInput != 0 || verticalInput != 0)
        {
            robo3Controller.SetBool("Run", true);
        }
        else
        {
            robo3Controller.SetBool("Run", false);
        }

        if (jumpInput != 0)
        {
            jumping = true;
        }
        
        if (jumping && transform.position.y <= 3)
        {
            //robo3Controller.SetBool("Jump", true);
            transform.Translate(Vector3.up * jumpForce * Time.deltaTime, Space.World);

            if (transform.position.y >= 3)
            {
                jumping = false;
            }
        }
        else if (!jumping && transform.position.y > 0)
        {
            //robo3Controller.SetBool("Jump", false);
            transform.Translate(Vector3.up * -jumpForce * Time.deltaTime, Space.World);
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

        UpdateCollider();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Touching Player");
        }
    }

    public void UpdateCollider()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        collider.sharedMesh = null;
        collider.sharedMesh = colliderMesh;
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
