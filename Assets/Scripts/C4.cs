using System.Collections;
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


    // Start is called before the first frame update
    void Start()
    {
        // Gets the Rigidbody2D attached to the C4
            rb = GetComponent<Rigidbody2D>();

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

        // Throws the C4
            rb.AddForce(new Vector2(forceForward, forceUpward));
        //C4 is set to default to not stuck to anything
            isStuck = false;
    }


    void Update()
    {
        // If C4 is stuck to someting and player detonates it, the C4 will explode
            if (Input.GetMouseButton(1) && isStuck)
                Destroy(gameObject, 0.1f);
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



    private void OnTriggerStay2D(Collider2D other)
    {
        // If the object in its radius can be destroyed and player has detonated the C4 and the C4 is stuck to the ground
            if ((explosionTags.Contains(other.gameObject.tag) && Input.GetMouseButtonDown(1) && isStuck))
            {
            // If fuse box is destroyed, the game ends
                if (other.gameObject.tag == "FuseBox")
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().RestartScene("Test_Scene");

                // Destroys objects in its radius that can be destroyed
                    Destroy(other.gameObject);

            }

    }
}
