using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoExplosionZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grenade" || other.gameObject.tag == "C4")
            Destroy(other.gameObject);
    }



}
