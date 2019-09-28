using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoExplosionZone : MonoBehaviour
{
    // Box that will disable the Disable
        public GameObject disableBox;


    void Update()
    {
        // If someone has destroyed the Disable Box, destroy the No-Explosion Zone
            if (disableBox == null)
                Destroy(gameObject);
    }

    // If an explosive enters the No-Explosion Zone it is destroyed
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grenade" || other.gameObject.tag == "C4")
            Destroy(other.gameObject);
    }



}
