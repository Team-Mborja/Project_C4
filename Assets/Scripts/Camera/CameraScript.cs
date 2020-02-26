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
        float xMax;
    // Max X Distance
        float yMax;
    // Timer for the panning camera to end
        float timer;
    // How long the panning camera is active for
        public float panCamera;
    // Camera Panned
        public bool cameraPanned;


    void Start()
    {
        // Gets the Player and Level Manager from the Game 
            player = GameObject.FindGameObjectWithTag("Player");
            managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
            cameraPanned = false;
    }


    void Update()
    {
        // Adds to the panning camera timer
            timer += Time.deltaTime;

        // Once the timer reaches a certain value, active the camera follow script
        if (timer >= panCamera && gameObject.GetComponent<Camera_Follow>() == null)
        {
            gameObject.AddComponent<Camera_Follow>();
            cameraPanned = true;
        }

       // Tracks the mouse position in game
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            float dist = 0;

            if (plane.Raycast(ray, out dist))
                cursor.transform.position = ray.GetPoint(dist);

            if (managerScript.pausedGame == true || managerScript.gameOver == true)
            {
            Cursor.visible = true;
            cursor.GetComponent<SpriteRenderer>().enabled = false;
            }
            else {
            Cursor.visible = false;
            cursor.GetComponent<SpriteRenderer>().enabled = true;
            }

        // Sets cursor maximums based on what equipment you are holding 
            if (managerScript.gameOver == false)
            {
                if (managerScript.weapons[player.GetComponent<Player>().slot].GetComponent<Grenade>() != null)
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

            // Max Distances
                if (cursor.transform.position.x - player.transform.position.x >= xMax)
                    cursor.transform.position = new Vector2(player.transform.position.x + xMax, cursor.transform.position.y);
                else if (cursor.transform.position.x - player.transform.position.x <= -xMax)
                    cursor.transform.position = new Vector2(player.transform.position.x - xMax, cursor.transform.position.y);

                if (cursor.transform.position.y - player.transform.position.y >= yMax)
                    cursor.transform.position = new Vector2(cursor.transform.position.x, player.transform.position.y + yMax);
                else if (cursor.transform.position.y - player.transform.position.y <= -yMax)
                    cursor.transform.position = new Vector2(cursor.transform.position.x, player.transform.position.y - yMax);

        }
    }
}
