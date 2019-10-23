using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public List<string> grounded = new List<string>();

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (grounded.Contains(collider.gameObject.tag) && (transform.localRotation.z % 90) == 0 && gameObject.GetComponent<Rigidbody2D>() != null)
            Destroy(gameObject.GetComponent<Rigidbody2D>());
    }


    private void OnCollisionStay2D(Collision2D collider)
    {
        if (grounded.Contains(collider.gameObject.tag) && (transform.localRotation.z % 90) == 0 && gameObject.GetComponent<Rigidbody2D>() != null)
            Destroy(gameObject.GetComponent<Rigidbody2D>());
    }

    private void OnCollisionExit2D(Collision2D collider)
    {
        if (grounded.Contains(collider.gameObject.tag))
            gameObject.AddComponent<Rigidbody2D>();
    }
}
