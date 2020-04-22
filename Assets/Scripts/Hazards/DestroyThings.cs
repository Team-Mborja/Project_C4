using System.Collections.Generic;
using UnityEngine;

public class DestroyThings : MonoBehaviour
{
    // List of objects the laser destroys
        public List<string> destroyList = new List<string>();

    // Function that checks if the tag is in the collider, then destroys it
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (destroyList.Contains(other.gameObject.tag))
            Destroy(other.gameObject);

        if (other.gameObject.tag == "Grenade")
            Destroy(other.gameObject.GetComponent<Grenade>().timerInstance);
    }
}