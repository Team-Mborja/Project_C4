using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float travelSpeed;

    // List of tags on objects that can be destroyed
        public List<string> explosionTags = new List<string>();

    // Start is called before the first frame update
    void Start()
    {

        // Checks if player is left or right
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().isLeft == true)
            travelSpeed = -Mathf.Abs(travelSpeed);

        transform.rotation = Quaternion.Euler(0,0,90);  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * travelSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "FuseBox")
            GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().RestartScene("Test_Scene");

        if (explosionTags.Contains(collision.gameObject.tag))
            Destroy(collision.gameObject);

        if(collision.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
