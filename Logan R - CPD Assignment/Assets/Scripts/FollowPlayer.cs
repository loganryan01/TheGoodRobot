/*------------------------------------
    File name: FollowPlayer.cs
    Purpose: Follow behind the player.
    Author: Logan Ryan
    Modified: 15 September 2020
--------------------------------------
    Copyright 2020 Logan Ryan
------------------------------------*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // Set the offset for the object
        offset = new Vector3(6, 4, -5);
    }

    // Update is called once per frame
    void Update()
    {
        // Follow the player on the z axis not on the x or y axis
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z + offset.z);
    }
}
