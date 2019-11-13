using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // LifeTime of the grenade
        public float lifeTime;
    //Countdown till destruction
        float destruct;
    // Force of the greande X-Axis
        float forceForward;
    // Force of the grenade Y-Axis
        float forceUpward;
    // Max Forward
        public float xMax;
    // Max Upward
        public float yMax;
    // Scale of the X-Axis throwing
        public float forwardMultiply;
    // Scale of the Y-Axis throwing
        public float upwardMultiply;
    // Name of Equipment
        public string itemName;
    // Has been throw
        bool isThrown;
    // Spawn Location
        public Vector2 offset;

    GameObject player;
    GameObject cursor;
    GameObject manager;

    public GameObject explodeObject;
    // Start is called before the first frame update
    void Start()
    {
        player  = GameObject.FindGameObjectWithTag("Player");
        cursor  = GameObject.FindGameObjectWithTag("Cursor");
        manager = GameObject.FindGameObjectWithTag("Manager");

        isThrown = false;
        
        // Sets destruct timer on
            destruct = lifeTime;

    }

    // Update is called once per frame
    void Update()
    {

        if (isThrown == false)
        {
            transform.position = new Vector2(player.transform.position.x + offset.x, player.transform.position.y + offset.y);

            // calculate force based on how far mouse is from player
            forceForward = Mathf.Abs(cursor.transform.position.x - player.transform.position.x) * forwardMultiply;
            forceUpward = Mathf.Abs(cursor.transform.position.y - player.transform.position.y) * upwardMultiply;

            // Checks if player is left or right
           if (player.GetComponent<Player>().isLeft == true)
            {
               forceForward = -Mathf.Abs(forceForward);
               offset.x = -Mathf.Abs(offset.x);
            }
           else
            {
                forceForward = Mathf.Abs(forceForward);
                offset.x = Mathf.Abs(offset.x);
            }

        }

        // timer counts down
            destruct -= Time.deltaTime;


        if (Input.GetMouseButtonUp(0) && isThrown == false)
        {
            ThrowGrenade();
            isThrown = true;
        }

        // checks if timer has ended and detonates if done
        if (destruct <= 0)
        {
            if(isThrown == false)
                Destroy(player);

            explodeObject.GetComponent<Explode>().Explosion();
        }

    }

    void ThrowGrenade()
    {
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();

        rb.AddForce(new Vector2(forceForward, forceUpward));

    }
}
