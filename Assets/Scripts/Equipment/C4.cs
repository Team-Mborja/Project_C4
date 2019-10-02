﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    // Creates a Rigidbody2D
        Rigidbody2D rb;
    // Foce on the X-Axis
        float forceForward;
    // Force on the Y-Axis
        float forceUpward;
    // Scale for throwing on the X-Axis
        public float forwardMultiply;
    // Scale for throwing on the Y-Axis
        public float upwardMultiply;
    // Max Forward
        public float xMax;
    // Max Upward
        public float yMax;
    // Detects if object is stuck to the ground
        public bool isStuck;
    // List of tags of objects that can explode to C4
        public List<string> explosionTags = new List<string>();
    // Items in Range
        List<GameObject> inRangeItems = new List<GameObject>();
    // Name of Equipment
        public string itemName;
    // Has the object been thrown
        bool isThrown;

    // Start is called before the first frame update
    void Start()
    {
        isThrown = false;
        //C4 is set to default to not stuck to anything
            isStuck = false;

        // Calculates force based on mouse position compared to player position
        forceForward = Mathf.Abs(Input.mousePosition.x - GameObject.FindGameObjectWithTag("Player").transform.position.x) * forwardMultiply;
            forceUpward = Mathf.Abs(Input.mousePosition.y - GameObject.FindGameObjectWithTag("Player").transform.position.y) * upwardMultiply;

        // Checks force with maximums
            if (forceForward > xMax)
                forceForward = xMax;

            if (forceUpward > yMax)
                forceUpward = yMax;

        // Checks if the player is facing left
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isLeft == true)
                forceForward = -Mathf.Abs(forceForward);

    }


    void Update()
    {

        if (Input.GetMouseButtonUp(0) && isThrown == false)
        {
            ThrowC4();
            isThrown = true;
        }

        // If C4 is stuck to someting and player detonates it, the C4 will explode
        if (Input.GetMouseButton(1) && isStuck)
            Explode();
    }


    public void ThrowC4()
    {

         rb = gameObject.AddComponent<Rigidbody2D>();

        // Throws the C4
        rb.AddForce(new Vector2(forceForward, forceUpward));
    }

    // Sticks C4 to floors
   private void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject.tag != "Player" && (collider.gameObject.tag == "Floor" && collider.gameObject.tag != "Weapon"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isStuck = true;
        }
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

    private void Explode()
    {

        foreach (GameObject objects in inRangeItems)
        {

            if (objects.tag == "FuseBox")
                GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().RestartScene("Test_Scene");

            if (objects.tag == "Box" && objects.GetComponent<BoxDrops>() != null)
                objects.GetComponent<BoxDrops>().DropItem();

            Destroy(objects);

            if (inRangeItems.Count == 0)
                break;

        }

        Destroy(gameObject);
    }

}
