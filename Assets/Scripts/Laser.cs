using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject disable_box;
    public GameObject laser_beam;
    public GameObject laser_end;


    private void Update()
    {
        if(disable_box == null)
            Destroy(gameObject);
        
    }


}