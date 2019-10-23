using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public List<string> grounded = new List<string>();

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(grounded.Contains(collider.gameObject.tag) && (transform.localRotation.z % 90) == 0)
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }


    private void OnCollisionStay2D(Collision2D collider)
    {
        if (grounded.Contains(collider.gameObject.tag) && (transform.localRotation.z % 90) == 0)
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (grounded.Contains(collider.gameObject.tag)) 
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}
