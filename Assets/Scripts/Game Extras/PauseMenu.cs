﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    //Text field for the title on the pause menu
        public Text title;
    // Buttons on the pause menu
        public List<Button> buttons = new List<Button>();

    // Activates pause screen when you press escape
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape) && gameObject.GetComponent<Image>().enabled == false && GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().gameOver == false)
           ActivatePauseScreen();

    }

    // Enables all relevant UI Elements for the pause screen
    void ActivatePauseScreen()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().pausedGame = true;
        gameObject.GetComponent<Image>().enabled = true;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(true);
        }
    }


    // Disables all relevant UI Elements for the pause screen
    public void DisablePauseScreen()
    {
        GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().pausedGame = false;
        gameObject.GetComponent<Image>().enabled = false;

        for (int j = 0; j < gameObject.transform.childCount; j++)
        {
            var child = gameObject.transform.GetChild(j).gameObject;
            child.SetActive(false);
        }
    }
}
