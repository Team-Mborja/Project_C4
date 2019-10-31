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
    public string specialObjective;
    LevelManager managerScript;
    Player playerScript;

    public int jumpMax;
    public int[] usedEquipment = new int[3];
    public GameObject protectedObject;
    // Start is called before the first frame update
    void Start()
    {
        starCount = 0;
        managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("Player") == null && gameObject.GetComponent<Image>().enabled == false)
        {
            levelStatus.text = "Level Failed";
            ActivateEndScreen();
        }

        if (GameObject.FindGameObjectWithTag("FuseBox") == null && gameObject.GetComponent<Image>().enabled == false)
        {
            levelStatus.text = "Level Complete";
            ActivateEndScreen();
        }

    }

    void ActivateEndScreen()
    {
        managerScript.gameOver = true;
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

            if (managerScript.timer <= parTime)
                starCount += 1;

            SpecialObjective();
        }
    }

    void SpecialObjective()
    {
        if (specialObjective == "One Grenade" && playerScript.usedEquipment[0] == 1 && playerScript.usedEquipment[1] == 0 && playerScript.usedEquipment[2] == 0)
            starCount += 1;
        else if (specialObjective == "Limited Jump" && playerScript.usedJump <= jumpMax)
            starCount += 1;
        else if (specialObjective == "Protected Object" && protectedObject != null)
            starCount += 1;

        
    }
}
