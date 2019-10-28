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
    // Max X Distance
        public float yMax;
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

        if (cursor.transform.position.x - player.transform.position.x >= xMax)
            cursor.transform.position = new Vector2(player.transform.position.x + xMax, cursor.transform.position.y);

        if (cursor.transform.position.x - player.transform.position.x <= -xMax)
            cursor.transform.position = new Vector2(player.transform.position.x - xMax, cursor.transform.position.y);

        if (cursor.transform.position.y - player.transform.position.y >= yMax)
            cursor.transform.position = new Vector2(cursor.transform.position.x, player.transform.position.y + yMax);

        if (cursor.transform.position.y - player.transform.position.y <= -yMax)
            cursor.transform.position = new Vector2(cursor.transform.position.x, player.transform.position.y - yMax);
    }
}
