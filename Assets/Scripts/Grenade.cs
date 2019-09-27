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

    public List<string> explosionTags = new List<string>();
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        destruct = lifeTime;

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isLeft == true)
            forceForward = -Mathf.Abs(forceForward);

        rb.AddForce(new Vector2(forceForward, forceUpward));
    }

    // Update is called once per frame
    void Update()
    {
        destruct -= Time.deltaTime;

        if (destruct <= 0)
            Destroy(gameObject);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if ((explosionTags.Contains(other.gameObject.tag) && destruct <= 0.01f))
        {
            if (other.gameObject.tag == "FuseBox")
                GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().RestartScene("Test_Scene");

            Destroy(other.gameObject);

        }
     
    }
}
