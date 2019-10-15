﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float travelSpeed;

    // List of tags on objects that can be destroyed
        public List<string> explosionTags = new List<string>();
    // Name of Equipment
        public string itemName;
    // Items in Range
        List<GameObject> inRangeItems = new List<GameObject>();
    // Target of the rocket
        Vector3 target;

    public GameObject player;
    public GameObject cursor;
    public GameObject manager;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        manager = GameObject.FindGameObjectWithTag("Manager");

        
        target = cursor.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
           
        // Checks if player is left or right
        if (player.GetComponent<Player>().isLeft == true)
            travelSpeed = -Mathf.Abs(travelSpeed);


    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, travelSpeed * (travelSpeed/ Mathf.Abs(travelSpeed)));
        target = Vector3.MoveTowards(target, transform.position, -(travelSpeed * (travelSpeed / Mathf.Abs(travelSpeed))));
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (explosionTags.Contains(other.gameObject.tag))
            inRangeItems.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (inRangeItems.Contains(other.gameObject))
            inRangeItems.Remove(other.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void Explode()
    {
            foreach (GameObject objects in inRangeItems)
            {

                if (objects.tag == "FuseBox")
                    manager.GetComponent<LevelManager>().RestartScene();

                if (objects.tag == "Box" && objects.GetComponent<BoxDrops>() != null)
                    objects.GetComponent<BoxDrops>().DropItem();

                Destroy(objects);

            if (inRangeItems.Count == 0)
                break;

        }
            Destroy(gameObject);
    }
}