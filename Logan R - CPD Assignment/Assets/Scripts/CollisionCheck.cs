using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    private RotateArms rotateArms;
    
    // Start is called before the first frame update
    void Start()
    {
        rotateArms = GameObject.Find("Player/Arms").GetComponent<RotateArms>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && rotateArms.rotateArmsUp ||
            other.gameObject.CompareTag("Enemy") && rotateArms.rotateArmsDown ||
            other.gameObject.CompareTag("Box") && rotateArms.rotateArmsUp ||
            other.gameObject.CompareTag("Box") && rotateArms.rotateArmsDown)
        {
            Destroy(other.gameObject);
        }
    }
}
