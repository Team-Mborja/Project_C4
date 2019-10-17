using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Item replacing the mouse cursor in game
        public GameObject cursor;
    // Player Object
        GameObject player;
    // Location of the mouse
        Vector3 target;
    // Max X Disance 
        public float xMax;
    void Start()
    {
        // Makes the mouse cursor invisible in the game
            Cursor.visible = false;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Gets the positon of the mouse in world position and puts the new cursor thereS
    void Update()
    {
        
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        target = new Vector3(-target.x, -target.y, target.z);
        cursor.transform.position = new Vector2(target.x, target.y);

        if(Mathf.Abs(cursor.transform.position.x - player.transform.position.x) > xMax)
        {
            //if(player.GetComponent<Player>().isLeft == false)
               // cursor.transform.position.x = player.transform.position.x + xMax;

        }
    }
}
