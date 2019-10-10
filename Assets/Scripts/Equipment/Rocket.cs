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
 
    // Start is called before the first frame update
    void Start()
    {
        // Checks if player is left or right
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isLeft == true)
            travelSpeed = -Mathf.Abs(travelSpeed);

        transform.rotation = Quaternion.Euler(0,0,90);  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * travelSpeed);
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
