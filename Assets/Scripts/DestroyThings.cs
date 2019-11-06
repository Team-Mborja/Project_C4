﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyThings : MonoBehaviour
{
    // List of Objects the laser destroys
        public List<string> destroyList = new List<string>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyList.Contains(other.gameObject.tag))
            Destroy(other.gameObject);
    }
}