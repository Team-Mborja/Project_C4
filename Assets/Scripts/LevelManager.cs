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
    // Indicator
        GameObject redLine;
    //Player
        GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        redLine = GameObject.FindGameObjectWithTag("RedLine");
    }

    // Update is called once per frame
    void Update()
    {
        // Displays name of the equipment and how many you have left
            inventory_Display[0].text = inventory[0].ToString();
            inventory_Display[1].text = inventory[1].ToString();
            inventory_Display[2].text = inventory[2].ToString();

        if (player.GetComponent<Player>().slot == 0)
            redLine.transform.position = new Vector2(-11, 3.5f);
        else if (player.GetComponent<Player>().slot == 1)
            redLine.transform.position = new Vector2(0, 3.5f);
        else if (player.GetComponent<Player>().slot == 2)
            redLine.transform.position = new Vector2(11, 3.5f);

        // Restart the level when you press "R"
        if (Input.GetKey(KeyCode.R))
                RestartScene("Test_Scene");
    }

    // Restarts Level Function
    public void RestartScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
