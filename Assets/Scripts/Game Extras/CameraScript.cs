using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Item replacing the mouse cursor in game
    public GameObject cursor;
    // Player Object
    GameObject player;
    // Game Manager
    LevelManager managerScript;
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
        managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
    }

    // Gets the positon of the mouse in world position and puts the new cursor thereS
    void Update()
    {

        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        target = new Vector3(-target.x, -target.y, target.z);
        cursor.transform.position = new Vector2(target.x, target.y);

        if (GameObject.FindGameObjectWithTag("FuseBox") == null || GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().pausedGame == true) {
                xMax = 1000;
                yMax = 1000;
            }
        else if (managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<Grenade>() != null)
            {
                xMax = managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<Grenade>().xMax;
                yMax = managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<Grenade>().yMax;
            }
            else if (managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<C4>() != null)
            {
                xMax = managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<C4>().xMax;
                yMax = managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<C4>().yMax;
            }
            else if (managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<Rocket_Launcher>() != null)
            {
                xMax = managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<Rocket_Launcher>().xMax;
                yMax = managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<Rocket_Launcher>().yMax;
            }

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
