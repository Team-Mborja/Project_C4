using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // List of tags on objects that can be destroyed
        public List<string> explosionTags = new List<string>();
    // Items in Range
        List<GameObject> inRangeItems = new List<GameObject>();
    // GameObjects for the explosive and the explosion
        public GameObject explosion;
        public GameObject parent;

    // Update is called once per frame
    void Update()
    {
        //  Sets the player warning active if the the player is in range
            if (inRangeItems.Contains(GameObject.FindGameObjectWithTag("Player")))
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().warning.SetActive(true);
            else
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().warning.SetActive(false);
    }

    // Adds objects to explode list if in range
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (explosionTags.Contains(other.gameObject.tag))
            inRangeItems.Add(other.gameObject);
    }

    // Removes objects to explode list if in range 
    private void OnTriggerExit2D(Collider2D other)
    {
        if (inRangeItems.Contains(other.gameObject))
            inRangeItems.Remove(other.gameObject);
    }

    // Destroys all objects on explode list and starts the explosion annimation
    public void Explosion()
    {

        foreach (GameObject objects in inRangeItems)
        {

            if (objects.tag == "Box" && objects.GetComponent<BoxDrops>() != null)
                objects.GetComponent<BoxDrops>().DropItem();

            Destroy(objects);

            if (inRangeItems.Count == 0)
                break;
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(parent);
    }
}
