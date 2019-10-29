using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Launcher : MonoBehaviour
{
    public GameObject rocket;
    public bool isLaunched;
    public Vector2 offset;

    GameObject player;
    GameObject cursor;
    public GameObject spawn;

    public float xMax;
    public float yMax;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        isLaunched = false;  
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(player.transform.position.x + offset.x, player.transform.position.y + offset.y);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(cursor.transform.position.y - transform.position.y, cursor.transform.position.x - transform.position.x) * Mathf.Rad2Deg)));

        // Checks if player is left or right
        if (player.GetComponent<Player>().isLeft == true)
        {
            offset.x = -Mathf.Abs(offset.x);
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
        else
        {
            offset.x = Mathf.Abs(offset.x);
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y), transform.localScale.z);
        }
           
        

        if (Input.GetMouseButtonUp(0) && isLaunched == false)
        {
            Instantiate(rocket,spawn.transform.position, Quaternion.Euler(0, 0, 90));
            Destroy(gameObject);
        }
    }
}
