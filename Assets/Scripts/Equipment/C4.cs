using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    // Creates a Rigidbody2D
        public Rigidbody2D rb;
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
    // Name of Equipment
        public string itemName;
    // Has the object been thrown
        bool isThrown;
    // GameObject for the explosions on the C4
        public GameObject explodeObject;
    // GameObjects for the player, cursor, and level manager
        GameObject player;
        GameObject cursor;
        GameObject manager;
    // Bool to check if the C4 is stuck to something
        public bool isStuck;
    // Tags for all objects the C4 can stick to
        public List<string> stickTags = new List<string>();

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
         anim = gameObject.GetComponent<Animator>();
        
        // Initalizes the player, cursor, and level manager
            player = GameObject.FindGameObjectWithTag("Player");
            cursor = GameObject.FindGameObjectWithTag("Cursor");
            manager = GameObject.FindGameObjectWithTag("Manager");

        // Sets the defautlt to not be thrown
            isThrown = false;
        // Sets the default to not be stuck
            isStuck = false;
    }


    // Function that is called every frame
    void Update()
    {

        // All code that runs when the C4 is in your hand
            if(isThrown == false)
            {
                // Changes the position once held to move with the cursor
                    transform.position = (cursor.transform.position - player.transform.position).normalized * 1.25f + player.transform.position;

                // Calculates force based on mouse position compared to player position
                    forceForward = Mathf.Abs(cursor.transform.position.x - player.transform.position.x) * forwardMultiply;
                    forceUpward = Mathf.Abs(cursor.transform.position.y - player.transform.position.y) * upwardMultiply;

                // Checks if the cursor is left or right of the player
                    if (player.GetComponent<Player>().isLeft == true)
                        forceForward = -Mathf.Abs(forceForward);
                    else
                        forceForward = Mathf.Abs(forceForward);

                // Check if the cursor is above or below the player
                    if (player.GetComponent<Player>().isUp == true)
                        forceUpward = Mathf.Abs(forceUpward);
                    else
                        forceUpward = -Mathf.Abs(forceUpward);

        }


        // Throws the C4 with left mouse
            if (Input.GetMouseButtonUp(0) && isThrown == false)
            {
                ThrowC4();
                isThrown = true;
                GetComponent<Animator>().enabled = true;
            }

        // If C4 is stuck to someting and player detonates it, the C4 will explode
            if (Input.GetMouseButton(1) && isStuck == true)
                explodeObject.GetComponent<Explode>().Explosion();
        anim.SetTrigger("explodetrigger");
    }

    // Adds a rigidbody and adds force to the C4
        public void ThrowC4()
        {

            rb = gameObject.AddComponent<Rigidbody2D>();
            gameObject.AddComponent<BoxCollider2D>();
            // Throws the C4
                rb.AddForce(new Vector2(forceForward, forceUpward));
        }

    // Code that is called when the C4 collides with an object it sticks to
        private void OnCollisionEnter2D(Collision2D collision)
        {

        //souundScript.PlaySoundFile(souundScript.c4_stick);

        // rotates to match the object it sticks to and locks it in place
        if (stickTags.Contains(collision.gameObject.tag))
                {
                    var contact = collision.contacts[0];
                    var rot = Quaternion.FromToRotation(transform.up, contact.normal);
                    transform.rotation *= rot;

                    rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    isStuck = true;
                    GetComponent<Animator>().enabled = false;
                }
            
    }
}
