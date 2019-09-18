using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody2D rb;

    public float lifeTime;
    float destruct;

    public float forceForward;
    public float forceUpward;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destruct = lifeTime;
        
        
        rb.AddForce(Vector2.right * forceForward);
        rb.AddForce(Vector2.up * forceUpward);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(destruct);
        destruct -= Time.deltaTime;

        if (destruct <= 0)
            Destroy(gameObject);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Box" && destruct <= 0.01f)
            Destroy(other.gameObject);


        
      
    }
}
