using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // UI for inventory on each equipment
        public Text[] inventory_Display = new Text[3];
    // Inventory for each of the equipments
        public int[] inventory = new int[3];
    // Weapons availible for the level
        public GameObject[] weapons;
    // Player
        GameObject player;
    // Timer Text
        public Text timerText;
    // Game timer
        public float timer;
    // FuseBox object
        GameObject fuseBox;

    // Creates pause variable
    public bool pausedGame;
    // Creates game over variable
    public bool gameOver;

    // Start is called once at the start
    void Start()
    {
        // Creates the tag for player
        player = GameObject.FindGameObjectWithTag("Player");
        // Creates the tag for Fusebox
        fuseBox = GameObject.FindGameObjectWithTag("FuseBox");
        // Sets the timer variable to 0
        timer = 0;
        // Sets the pauseGame function bool to false
        pausedGame = false;
        // Sets the gameOver function to false
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Creates a timer in a string
        timerText.text = timer.ToString();

        // runs the level timer when the level is incomplete
            if (gameOver == false && pausedGame == false && Camera.main.GetComponent<CameraScript>().cameraPanned == true)
                timer += Time.deltaTime;

        if (inventory[0] <= 0 && inventory[1] <= 0 && inventory[2] <= 0 && GameObject.FindGameObjectWithTag("FuseBox") && GameObject.FindGameObjectWithTag("Grenade") == null && GameObject.FindGameObjectWithTag("C4") == null && GameObject.FindGameObjectWithTag("Rocket") == null)
            gameOver = true;

        // Displays name of the equipment and how many you have left
            inventory_Display[0].text = inventory[0].ToString();
            inventory_Display[1].text = inventory[1].ToString();
            inventory_Display[2].text = inventory[2].ToString();

        // If the player tag is still in the scene display which equuiment you ar using in the UI 
        if (player != null)
        {
            switch (player.GetComponent<Player>().slot)
            {
                case 0:
                    inventory_Display[0].color = Color.red;
                    inventory_Display[1].color = Color.white;
                    inventory_Display[2].color = Color.white;
                    break;
                case 1:
                    inventory_Display[0].color = Color.white;
                    inventory_Display[1].color = Color.red;
                    inventory_Display[2].color = Color.white;
                    break;
                case 2:
                    inventory_Display[0].color = Color.white;
                    inventory_Display[1].color = Color.white;
                    inventory_Display[2].color = Color.red;
                    break;


            }
        }
    }
}