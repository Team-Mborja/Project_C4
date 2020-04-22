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
    // Float for the par time of the level
        public float parTime;
    // Scripts of the level manager and player
        LevelManager managerScript;
        Player playerScript;
    // Total equipments used
    public int usedEquipment;

    // Start is called before the first frame update
    void Start()
    {
        managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // Activates the end scren with level failed
        if (GameObject.FindGameObjectWithTag("Player") == null && gameObject.GetComponent<Image>().enabled == false /*|| managerScript.gameOver == true*/)
        {
            status[0].GetComponent<Image>().enabled = true;
            ActivateEndScreen();
        }
        
        // Activates the end screen with level complete
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

        
    }

    // Sets star count for destoying the fuse box, beating the par time, and completing the special objective
    public void AwardStars()
    {
        if (GameObject.FindGameObjectWithTag("FuseBox") == null)
        {
            stars[0].GetComponent<Animator>().enabled = true;

            if (managerScript.timer <= parTime)
                stars[1].GetComponent<Animator>().enabled = true; ;

            if (playerScript.usedEquipment[0] + playerScript.usedEquipment[1] + playerScript.usedEquipment[2] == usedEquipment)
                stars[2].GetComponent<Animator>().enabled = true;
        }
    }

   
        
    

        
    
}
