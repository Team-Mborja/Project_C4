using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public Text[] inventory_Display = new Text[2];
    public int[] inventory = new int[2];
    public GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        inventory_Display[0].text = "Grendades: " + inventory[0].ToString();
        inventory_Display[1].text = "C4s: " + inventory[1].ToString();

        if (Input.GetKey(KeyCode.R))
            SceneManager.LoadScene("Test_Scene");
    }
}
