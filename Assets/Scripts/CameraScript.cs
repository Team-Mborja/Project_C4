﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Item replacing the mouse cursor in game
        public GameObject cursor;
    // Location of the mouse
        Vector3 target;
    void Start()
    {
        // Makes the mouse cursor invisible in the game
            Cursor.visible = false;
    }

    // Gets the positon of the mouse in world position and puts the new cursor thereS
    void Update()
    {
        
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        target = new Vector3(-target.x, -target.y, target.z);
        cursor.transform.position = new Vector2(target.x, target.y);
    }
}