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

    float timer;
    public float panCamera;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
    }


    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= panCamera && gameObject.GetComponent<Camera_Follow>() == null)
            gameObject.AddComponent<Camera_Follow>();

       
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, Vector3.zero);
            float dist = 0;

            if (plane.Raycast(ray, out dist))
                cursor.transform.position = ray.GetPoint(dist);

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
