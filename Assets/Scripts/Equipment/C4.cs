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

    public GameObject stuck;

     GameObject player;
     GameObject cursor;
     GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        manager = GameObject.FindGameObjectWithTag("Manager");


        // Sets the defautlt to not be thrown
        isThrown = false;

    }


    void Update()
    {

        if(isThrown == false)
        {
            transform.position = new Vector2(player.transform.position.x + offset.x, player.transform.position.y + offset.y);

            // Calculates force based on mouse position compared to player position
            forceForward = Mathf.Abs(GameObject.FindGameObjectWithTag("Cursor").transform.position.x - GameObject.FindGameObjectWithTag("Player").transform.position.x) * forwardMultiply;
            forceUpward = Mathf.Abs(GameObject.FindGameObjectWithTag("Cursor").transform.position.y - GameObject.FindGameObjectWithTag("Player").transform.position.y) * upwardMultiply;

            // Checks force with maximums
            if (forceForward > xMax)
                forceForward = xMax;

            if (forceUpward > yMax)
                forceUpward = yMax;


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
        }

        // If C4 is stuck to someting and player detonates it, the C4 will explode
        if (Input.GetMouseButton(1) && stuck.GetComponent<C4_Stick>().isStuck)
            Explode();
    }


    public void ThrowC4()
    {

         rb = gameObject.AddComponent<Rigidbody2D>();

        // Throws the C4
        rb.AddForce(new Vector2(forceForward, forceUpward));
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

            if (objects.tag == "FuseBox")
                GameObject.FindGameObjectWithTag("Menu").GetComponent<MenuManager>().OpenScene("Title");

            if (objects.tag == "Box" && objects.GetComponent<BoxDrops>() != null)
                objects.GetComponent<BoxDrops>().DropItem();

            Destroy(objects);

            if (inRangeItems.Count == 0)
                break;

        }

        Destroy(gameObject);
    }

}
