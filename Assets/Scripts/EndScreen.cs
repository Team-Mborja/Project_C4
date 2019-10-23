using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    public Text levelStatus;
    public Button restartButton;
    public Button mainMenuButton;

    public Image[] stars = new Image[3];
    int starCount;
    public float parTime;

    // Start is called before the first frame update
    void Start()
    {
        starCount = 0;
        AwardStars();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            levelStatus.text = "Level Failed";
            ActivateEndScreen();
        }

        if (GameObject.FindGameObjectWithTag("FuseBox") == null)
        {
            levelStatus.text = "Level Complete";
            ActivateEndScreen();
        }

    }

    void ActivateEndScreen()
    {
        gameObject.GetComponent<Image>().enabled = true;

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            var child = gameObject.transform.GetChild(i).gameObject;
            child.SetActive(true);
        }

        AwardStars();

        switch(starCount)
        {
            case 0:
                stars[0].GetComponent<Image>().enabled = false;
                stars[1].GetComponent<Image>().enabled = false;
                stars[2].GetComponent<Image>().enabled = false;
                break;
            case 1:
                stars[0].GetComponent<Image>().enabled = true;
                stars[1].GetComponent<Image>().enabled = false;
                stars[2].GetComponent<Image>().enabled = false;
                break;
            case 2:
                stars[0].GetComponent<Image>().enabled = true;
                stars[1].GetComponent<Image>().enabled = true;
                stars[2].GetComponent<Image>().enabled = false;
                break;
            case 3:
                stars[0].GetComponent<Image>().enabled = true;
                stars[1].GetComponent<Image>().enabled = true;
                stars[2].GetComponent<Image>().enabled = true;
                break;
        }
    }

    public void AwardStars()
    {
        if (GameObject.FindGameObjectWithTag("FuseBox") == null)
        {
            starCount += 1;

            if (GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().timer <= parTime)
                starCount += 1;

            if (GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().inventory[0] == 0 && GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().inventory[1] == 2 && GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().inventory[2] == 0)
                starCount += 1;

        }
    }
}
