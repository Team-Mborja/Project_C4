using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4 : MonoBehaviour
{
    Rigidbody2D rb;

    public float forceForward;
    public float forceUpward;
    // Detects if object is stuck to the ground
        public bool isStuck;
    // List of tags of objects that can explode to C4
        public List<string> explosionTags = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(forceForward, forceUpward));
        isStuck = false;
   
    }


    void Update()
    {
        if (Input.GetMouseButton(1) && isStuck)
            Destroy(gameObject);
    }

   private void OnCollisionEnter2D(Collision2D collider)
    {

        if (collider.gameObject.tag != "Player" && (collider.gameObject.tag == "Floor" && collider.gameObject.tag != "Weapon"))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            isStuck = true;
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if ((explosionTags.Contains(other.gameObject.tag) && Input.GetMouseButtonDown(1) && isStuck))
        {
            if (other.gameObject.tag == "FuseBox")
                GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().RestartScene("Test_Scene");

            Destroy(other.gameObject);

        }

    }
}
