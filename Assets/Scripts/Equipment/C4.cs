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

    public GameObject explodeObject;

     GameObject player;
     GameObject cursor;
     GameObject manager;

    public bool isStuck;
    public List<string> stickTags = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        manager = GameObject.FindGameObjectWithTag("Manager");


        // Sets the defautlt to not be thrown
        isThrown = false;
        isStuck = false;

    }


    void Update()
    {

        if(isThrown == false)
        {
            transform.position = (cursor.transform.position - player.transform.position).normalized * 1.25f + player.transform.position;
            // Calculates force based on mouse position compared to player position
            forceForward = Mathf.Abs(cursor.transform.position.x - player.transform.position.x) * forwardMultiply;
            forceUpward = Mathf.Abs(cursor.transform.position.y - player.transform.position.y) * upwardMultiply;

            // Checks if player is left or right
            if (player.GetComponent<Player>().isLeft == true)
                forceForward = -Mathf.Abs(forceForward);
            else
                forceForward = Mathf.Abs(forceForward);

            if (player.GetComponent<Player>().isUp == true)
                forceUpward = Mathf.Abs(forceUpward);
            else
                forceUpward = -Mathf.Abs(forceUpward);

        }


        // Checks if player can throw C4
        if (Input.GetMouseButtonUp(0) && isThrown == false)
        {
            ThrowC4();
            isThrown = true;
            GetComponent<Animator>().enabled = true;
        }

        // If C4 is stuck to someting and player detonates it, the C4 will explode
        if (Input.GetMouseButton(1) && isStuck == true)
            explodeObject.GetComponent<Explode>().Explosion();

    }


    public void ThrowC4()
    {

         rb = gameObject.AddComponent<Rigidbody2D>();

        // Throws the C4
        rb.AddForce(new Vector2(forceForward, forceUpward));
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
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
