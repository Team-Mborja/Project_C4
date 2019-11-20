using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    // Float for speed of the rocekt
        public float travelSpeed;
    // Name of Equipment
        public string itemName;
    // Target of the rocket
        Vector3 target;
    // GameObjects for player, cursor, anf level manager
        GameObject player;
        GameObject cursor;
        GameObject manager;
    // GameObject for the explosions on the Rocket 
        public GameObject explosionObject;
    // Raycast at the front of the rocekt
        RaycastHit2D front;

    // Start is called before the first frame update
    void Start()
    {
        // Initalize the player, cursor, and level manager
            player = GameObject.FindGameObjectWithTag("Player");
            cursor = GameObject.FindGameObjectWithTag("Cursor");
            manager = GameObject.FindGameObjectWithTag("Manager");

        // Defaults the target to the cursor
            target = cursor.transform.position;
        // Rotates the rocket towards the player
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
           
        // Checks if player is left or right
            if (player.GetComponent<Player>().isLeft == true)
                travelSpeed = -Mathf.Abs(travelSpeed);

       // Sets the rotation again
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(cursor.transform.position.y - transform.position.y, cursor.transform.position.x - transform.position.x) * Mathf.Rad2Deg) + 90));
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the RayCast
            front = Physics2D.Raycast(transform.position, Vector2.left, 0.75f);

        // If there nothing in front, move forward
            if (front.collider == null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, travelSpeed * (travelSpeed / Mathf.Abs(travelSpeed)) * Time.deltaTime);
                target = Vector3.MoveTowards(target, transform.position, -(travelSpeed * (travelSpeed / Mathf.Abs(travelSpeed))) * Time.deltaTime);
            }
            else
            {
            // Destroy the rocekt if it collides with something 
                if (front.collider.tag == "Laser")
                    Destroy(gameObject);
                else
                    explosionObject.GetComponent<Explode>().Explosion();
            }
    }
}
