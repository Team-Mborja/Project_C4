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

    // GameObjects for the player, cursor, and level manager
        GameObject player;
        GameObject cursor;
        GameObject manager;

    // GameObject for the explosions on the grenade
        public GameObject explodeObject;


    // Start is called before the first frame update
    void Start()
    {
        // Initalizes the player, cursor, and level manager
            player  = GameObject.FindGameObjectWithTag("Player");
            cursor  = GameObject.FindGameObjectWithTag("Cursor");
            manager = GameObject.FindGameObjectWithTag("Manager");

        // Defaults the grenade to not be thrown
            isThrown = false;
        
        // Sets destruct timer on
            destruct = lifeTime;

    }

    // Update is called once per frame
    void Update()
    {
        // When the grenade is held in your hand
            if (isThrown == false)
            {
                // Changes the position once held to move with the cursor
                    transform.position = (cursor.transform.position - player.transform.position).normalized  + player.transform.position;

                // calculate force based on how far mouse is from player
                    forceForward = Mathf.Abs(cursor.transform.position.x - player.transform.position.x) * forwardMultiply;
                    forceUpward = Mathf.Abs(cursor.transform.position.y - player.transform.position.y) * upwardMultiply;


                // Checks if the cursor is left or right of the player
                    if (player.GetComponent<Player>().isLeft == true)
                        forceForward = -Mathf.Abs(forceForward);
                    else
                        forceForward = Mathf.Abs(forceForward);

                // Checks if the cursor is above or below the player
                    if (player.GetComponent<Player>().isUp == true)
                        forceUpward = Mathf.Abs(forceUpward);
                    else
                        forceUpward = -Mathf.Abs(forceUpward);

            }

        // timer counts down
            destruct -= Time.deltaTime;

        // Throws the grenade if you press left mouse
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

    // Adds a rigidbody and adds force to the grenade
        void ThrowGrenade()
        {
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(forceForward, forceUpward));
        }
}
