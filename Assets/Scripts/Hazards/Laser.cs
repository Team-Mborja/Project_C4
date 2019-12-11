using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Creates a top to the laser script
    public GameObject disable_box;
    // Creates a laser beam to destroy objects that collide
    public GameObject laser_beam;
    // Created collider bottom and an animation to the laser beam
    public GameObject laser_end;

    // Function that if there is no dissable box the entire laser is destroyed
    private void Update()
    {
        if(disable_box == null)
            Destroy(gameObject);
        
    }
}