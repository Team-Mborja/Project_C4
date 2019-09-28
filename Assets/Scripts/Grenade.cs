using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Creates Rigidbody2D
        Rigidbody2D rb;
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
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

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

    // throws grenade
        rb.AddForce(new Vector2(forceForward, forceUpward));

        Debug.Log("Forward: " + forceForward);
        Debug.Log("Upward: " + forceUpward);
    }

    // Update is called once per frame
    void Update()
    {
        // timer counts down
            destruct -= Time.deltaTime;

        // checks if timer has ended and detonates if done
            if (destruct <= 0)
                Destroy(gameObject);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        // Checks of object in its radius can be destroyed and timer has ended
            if ((explosionTags.Contains(other.gameObject.tag) && destruct <= 0.01f))
            {
            // Checks of the fuse box was destroyed and ends the game if it was
                if (other.gameObject.tag == "FuseBox")
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().RestartScene("Test_Scene");

                // Destroys the objects in radius that can be destroyed
                    Destroy(other.gameObject);

            }
     
    }
}
