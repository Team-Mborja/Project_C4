using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // List of tags on objects that can be destroyed
        public List<string> explosionTags = new List<string>();
    // Items in Range
        List<GameObject> inRangeItems = new List<GameObject>();

        public GameObject explosion;
        public GameObject parent;

    // Update is called once per frame
    void Update()
    {

        if (inRangeItems.Contains(GameObject.FindGameObjectWithTag("Player")))
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().warning.SetActive(true);
        else
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().warning.SetActive(false);
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
