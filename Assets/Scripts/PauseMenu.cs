using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public Text title;
    public List<Button> buttons = new List<Button>();

    
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape) && gameObject.GetComponent<Image>().enabled == false)
           ActivatePauseScreen();

    }

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
