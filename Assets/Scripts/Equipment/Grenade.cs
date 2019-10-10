using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // LifeTime of the grenade
        public float lifeTime;
    //Countdown till destruction
        float destruct;
    // Force of the greande X-Axis
        float forceForward;
    // Force of the grenade Y-Axis
        float forceUpward;
    // Max Forward
        public float xMax;
    // Max Upward
        public float yMax;
    // Scale of the X-Axis throwing
     public float forwardMultiply;
    // Scale of the Y-Axis throwing
        public float upwardMultiply;
    // List of tags on objects that can be destroyed
        public List<string> explosionTags = new List<string>();
    // Items in Range
        List<GameObject> inRangeItems = new List<GameObject>();
    // Name of Equipment
        public string itemName;
    // Has been throw
        bool isThrown;

    // Start is called before the first frame update
    void Start()
    {
        isThrown = false;

        // Sets destruct timer on
            destruct = lifeTime;

        // calculate force based on how far mouse is from player
        forceForward = Mathf.Abs(Input.mousePosition.x - GameObject.FindGameObjectWithTag("Player").transform.position.x) * forwardMultiply;
        forceUpward = Mathf.Abs(Input.mousePosition.y - GameObject.FindGameObjectWithTag("Player").transform.position.y) * upwardMultiply;

        // Checks force against max distance
        if (forceForward > xMax)
            forceForward = xMax;

        if (forceUpward > yMax)
            forceUpward = yMax;

        // Checks if player is left or right
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isLeft == true)
            forceForward = -Mathf.Abs(forceForward);
    }

    // Update is called once per frame
    void Update()
    {

        // timer counts down
            destruct -= Time.deltaTime;

        if (Input.GetMouseButtonUp(0) && isThrown == false)
        {
            ThrowGrenade();
            isThrown = true;
        }

        // checks if timer has ended and detonates if done
        if (destruct <= 0)
            Explode();

    }

    void ThrowGrenade()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(forceForward, forceUpward));

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

       foreach(GameObject objects in inRangeItems)
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
