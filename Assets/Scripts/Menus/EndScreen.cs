using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    // Text field of the level completion status
        public Text levelStatus;
    // Buttons on the End Screen
        public Button restartButton;
        public Button mainMenuButton;
    // Star images on the end screen
        public Image[] stars = new Image[3];
    // Int for how many stars you have 
        int starCount;
    // Float for the par time of the level
        public float parTime;
    // String of the name of the special objective
        public string specialObjective;
    // Scripts of the level manager and player
        LevelManager managerScript;
        Player playerScript;
    // Max jumps allowed for max jumps objective 
        public int jumpMax;
    // Total equipments used
        public int[] usedEquipment = new int[3];
    // Protected GameObject for that special objective
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
        // Activates the end screnn with level failed
        if (GameObject.FindGameObjectWithTag("Player") == null && gameObject.GetComponent<Image>().enabled == false || managerScript.gameOver == true)
        {
            levelStatus.text = "Level Failed";
            ActivateEndScreen();
        }

        // Activates the end screnn with level failed
        if (GameObject.FindGameObjectWithTag("FuseBox") == null && gameObject.GetComponent<Image>().enabled == false)
        {
            levelStatus.text = "Level Complete";
            ActivateEndScreen();
        }

    }
    // Activates the end screeen and awards stars
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

    // Sets star count for destoying the fuse box, beating the par time, and completing the special objective
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

    // Checking the special objective
    void SpecialObjective()
    {
        if (specialObjective == "One Grenade" && playerScript.usedEquipment[0] == 1 && playerScript.usedEquipment[1] == 0 && playerScript.usedEquipment[2] == 0) // Was only one grenade used
            starCount += 1;
        else if (specialObjective == "Limited Jump" && playerScript.usedJump <= jumpMax) // Was the correct amount of jumps used
            starCount += 1;
        else if (specialObjective == "Protected Object" && protectedObject != null) // Was the protected object saved
            starCount += 1;

        
    }
}
