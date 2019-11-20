using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Launcher : MonoBehaviour
{
    // GameObject of the rocket
        public GameObject rocket;
    // Bool to check if the rocket laucher was fired
        public bool isLaunched;
    // Vector2 to decide where to spawn the rocket launcher
        public Vector2 offset;
    // GameObjects for the player and cursor
        GameObject player;
        GameObject cursor;
    // GameObject for the spawn location of rocekt
        public GameObject spawn;
    // Max values for the cursor on the rocket
        public float xMax;
        public float yMax;

    // Start is called before the first frame update
    void Start()
    {
        // Initiates the player and cursor
            player = GameObject.FindGameObjectWithTag("Player");
            cursor = GameObject.FindGameObjectWithTag("Cursor");
        // Defaults isLauched to false
            isLaunched = false;  
    }

    // Update is called once per frame
    void Update()
    {
        // Updates the position of the rocket laucher
            transform.position = new Vector2(player.transform.position.x + offset.x, player.transform.position.y + offset.y);
        // Rotates the rocket laucher towards the cursor
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(cursor.transform.position.y - transform.position.y, cursor.transform.position.x - transform.position.x) * Mathf.Rad2Deg)));

        // Checks if cursor is left or right of the player
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
           
        
        // Lauches the rocekt if you press left mouse
            if (Input.GetMouseButtonUp(0) && isLaunched == false)
            {
                Instantiate(rocket,spawn.transform.position, Quaternion.Euler(0, 0, 90));
                Destroy(gameObject);
            }
    }
}
