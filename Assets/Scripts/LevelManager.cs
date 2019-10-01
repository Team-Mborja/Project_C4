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

    // Update is called once per frame
    void Update()
    {
        // Displays name of the equipment and how many you have left
            inventory_Display[0].text = "Grendades: " + inventory[0].ToString();
            inventory_Display[1].text = "C4s: " + inventory[1].ToString();
            inventory_Display[2].text = "Rockets: " + inventory[2].ToString();

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
