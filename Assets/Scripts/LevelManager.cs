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
    //Player
        GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Displays name of the equipment and how many you have left
            inventory_Display[0].text = inventory[0].ToString();
            inventory_Display[1].text = inventory[1].ToString();
            inventory_Display[2].text = inventory[2].ToString();

        switch(player.GetComponent<Player>().slot)
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

        // Restart the level when you press "R"
        if (Input.GetKey(KeyCode.R))
                RestartScene();
    }

    // Restarts Level Function
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
