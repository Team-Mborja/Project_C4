using System.Collections;
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

     GameObject player;
     GameObject cursor;
     GameObject manager;

    public GameObject explode;
    RaycastHit2D front;

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

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(cursor.transform.position.y - transform.position.y, cursor.transform.position.x - transform.position.x) * Mathf.Rad2Deg) + 90));
    }

    // Update is called once per frame
    void Update()
    {
        front = Physics2D.Raycast(transform.position, Vector2.left, 0.75f);

        if (front.collider == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, travelSpeed * (travelSpeed / Mathf.Abs(travelSpeed)) * Time.deltaTime);
            target = Vector3.MoveTowards(target, transform.position, -(travelSpeed * (travelSpeed / Mathf.Abs(travelSpeed))) * Time.deltaTime);
        }
        else
        {
            if(front.collider.tag != "Laser")
                Explode();
        }


        if (inRangeItems.Contains(player))
            player.GetComponent<Player>().warning.SetActive(true);
        else
            player.GetComponent<Player>().warning.SetActive(false);

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

                if (objects.tag == "Box" && objects.GetComponent<BoxDrops>() != null)
                    objects.GetComponent<BoxDrops>().DropItem();

                Destroy(objects);

            if (inRangeItems.Count == 0)
                break;

        }
        Instantiate(explode, new Vector2(transform.position.x + 0.6f, transform.position.y), Quaternion.identity);
        Destroy(gameObject);
    }
}
