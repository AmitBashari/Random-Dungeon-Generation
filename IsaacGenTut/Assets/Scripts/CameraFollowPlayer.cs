﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;


    void Start()
    {

        offset = transform.position - player.transform.position + (new Vector3(0, 0, 0));
    }


    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
