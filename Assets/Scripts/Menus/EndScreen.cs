using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    // Text field of the level completion status
        public Image[] status = new Image[2];
    // Buttons on the End Screen
        public Button restartButton;
        public Button mainMenuButton;
    // Star images on the end screen
        public Image[] stars = new Image[3];
    // Int for how many stars you have 
        public int starCount;
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
        if (GameObject.FindGameObjectWithTag("Player") == null && gameObject.GetComponent<Image>().enabled == false /*|| managerScript.gameOver == true*/)
        {
            status[0].GetComponent<Image>().enabled = true;
            ActivateEndScreen();
        }
        
        if (GameObject.FindGameObjectWithTag("FuseBox") == null && gameObject.GetComponent<Image>().enabled == false)
        {
            status[1].GetComponent<Image>().enabled = true;
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
                stars[0].GetComponent<Animator>().enabled = false;
                stars[1].GetComponent<Animator>().enabled = false;
                stars[2].GetComponent<Animator>().enabled = false;
                break;
            case 1:
                stars[0].GetComponent<Animator>().enabled = true;
                stars[1].GetComponent<Animator>().enabled = false;
                stars[2].GetComponent<Animator>().enabled = false;
                break;
            case 2:
                stars[0].GetComponent<Animator>().enabled = true;
                stars[1].GetComponent<Animator>().enabled = true;
                stars[2].GetComponent<Animator>().enabled = false;
                break;
            case 3:
                stars[0].GetComponent<Animator>().enabled = true;
                stars[1].GetComponent<Animator>().enabled = true;
                stars[2].GetComponent<Animator>().enabled = true;
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
        if (specialObjective == "Two C4" && playerScript.usedEquipment[0] == 0 && playerScript.usedEquipment[1] == 2 && playerScript.usedEquipment[2] == 0) // Was only one grenade used
            starCount += 1;
        else if (specialObjective == "Limited Jump" && playerScript.usedJump <= jumpMax) // Was the correct amount of jumps used
            starCount += 1;
        else if (specialObjective == "Protected Object" && protectedObject != null) // Was the protected object saved
            starCount += 1;

        
    }
}
