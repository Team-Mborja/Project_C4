using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // List of tags on objects that can be destroyed
    public List<string> explosionTags = new List<string>();
    public List<string> animateTags = new List<string>();
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
        Invoke("DestroyPieces", 4.0f);
        //  Explode.GetComponent<Rocket.cs>().FragmentObject();

        foreach (GameObject objects in inRangeItems)
        {

            if (objects.tag == "Box" && objects.GetComponent<BoxDrops>() != null)
                objects.GetComponent<BoxDrops>().DropItem();

            if (animateTags.Contains(objects.tag) && objects.GetComponent<Animator>() != null)
            {
                objects.GetComponent<Animator>().SetTrigger("Destroyed");
                Destroy(objects, 1.0f);
            }

            if(objects.GetComponent<Explodable>() != null)
            {
                objects.GetComponent<Explodable>().explode();
            }








             else
                 Destroy(objects);









            if (inRangeItems.Count == 0)
                break;
        }
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(parent);
    }
    void DestroyPieces()
    {
        GameObject[] destroy = GameObject.FindGameObjectsWithTag("piecefordestroy");
        foreach (GameObject dest in destroy)
            GameObject.Destroy(dest);
    }
}
