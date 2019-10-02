using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Box that will disable the Disable
        public GameObject disableBoxLaser;
    // List of Objects the laser destroys
        public List<string> destroyList = new List<string>();
    void Update()
    {
        // If someone has destroyed the Disable Box (Laser), destroy the Laser
        if (disableBoxLaser == null)
            Destroy(gameObject);
    }

    // If an explosive enters the No-Explosion Zone it is destroyed
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyList.Contains(other.gameObject.tag))
            Destroy(other.gameObject);
    }



}