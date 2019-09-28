using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoExplosionZone : MonoBehaviour
{
    public GameObject disableBox;


    void Update()
    {
        if (disableBox == null)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Grenade" || other.gameObject.tag == "C4")
            Destroy(other.gameObject);
    }



}
