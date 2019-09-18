using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    Rigidbody2D rb;

    public float forceForward;
    public float forceUpward;

    public bool isStuck;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(forceForward, forceUpward));
        isStuck = false;
   
    }

   private void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject.tag != "Player" && (collider.gameObject.tag == "Floor" && collider.gameObject.tag != "Weapon"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isStuck = true;
        }
    }
}
