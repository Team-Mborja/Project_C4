using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
        public float travelSpeed;
    // Name of Equipment
        public string itemName;
    // Target of the rocket
        Vector3 target;

     GameObject player;
     GameObject cursor;
     GameObject manager;

    public GameObject explosionObject;
    RaycastHit2D front;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cursor = GameObject.FindGameObjectWithTag("Cursor");
        manager = GameObject.FindGameObjectWithTag("Manager");

        
        target = cursor.transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg);
           
        // Checks if player is left or right
        if (player.GetComponent<Player>().isLeft == true)
            travelSpeed = -Mathf.Abs(travelSpeed);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(cursor.transform.position.y - transform.position.y, cursor.transform.position.x - transform.position.x) * Mathf.Rad2Deg) + 90));
    }

    // Update is called once per frame
    void Update()
    {
        front = Physics2D.Raycast(transform.position, Vector2.left, 0.75f);

        if (front.collider == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, travelSpeed * (travelSpeed / Mathf.Abs(travelSpeed)) * Time.deltaTime);
            target = Vector3.MoveTowards(target, transform.position, -(travelSpeed * (travelSpeed / Mathf.Abs(travelSpeed))) * Time.deltaTime);
        }
        else
        {
            if(front.collider.tag != "Laser")
                explosionObject.GetComponent<Explode>().Explosion();
        }
    }
}
