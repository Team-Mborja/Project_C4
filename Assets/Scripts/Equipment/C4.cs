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
    // List of tags of objects that can explode to C4
        public List<string> explosionTags = new List<string>();
    // Items in Range
        List<GameObject> inRangeItems = new List<GameObject>();
    // Name of Equipment
        public string itemName;
    // Has the object been thrown
        bool isThrown;
    // Spawn Offset
        public Vector2 offset;

    public GameObject explode;

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
            transform.position = new Vector2(player.transform.position.x + offset.x, player.transform.position.y + offset.y);

            // Calculates force based on mouse position compared to player position
            forceForward = Mathf.Abs(cursor.transform.position.x - player.transform.position.x) * forwardMultiply;
            forceUpward = Mathf.Abs(cursor.transform.position.y - player.transform.position.y) * upwardMultiply;

            // Checks if player is left or right
            if (player.GetComponent<Player>().isLeft == true)
            {
                forceForward = -Mathf.Abs(forceForward);
                offset.x = -Mathf.Abs(offset.x);
            }
            else
            {
                forceForward = Mathf.Abs(forceForward);
                offset.x = Mathf.Abs(offset.x);
            }

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
            Explode();

        if (inRangeItems.Contains(player))
            player.GetComponent<Player>().warning.SetActive(true);
        else
            player.GetComponent<Player>().warning.SetActive(false);
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
    // Puts explodable objects in range set to explode
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (explosionTags.Contains(other.gameObject.tag))
            inRangeItems.Add(other.gameObject);
    }

    // Takes explodable objects in range out of range
    private void OnTriggerExit2D(Collider2D other)
    {
        if (inRangeItems.Contains(other.gameObject))
            inRangeItems.Remove(other.gameObject);
    }

    // Explodes the C4 and all objects in its range
    private void Explode()
    {

        foreach (GameObject objects in inRangeItems)
        {

            if (objects.tag == "Box" && objects.GetComponent<BoxDrops>() != null)
                objects.GetComponent<BoxDrops>().DropItem();

            Destroy(objects);

            if (inRangeItems.Count == 0)
                break;

        }
        Instantiate(explode, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
