using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        if (playerController != null)
        {
            Debug.Log("Player Controller has been found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground"))
    //    {
    //        playerController.isOnGround = true;
    //    }

    //    if (collision.gameObject.CompareTag("Enemy") ||
    //        collision.gameObject.CompareTag("Box"))
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}
}
