using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    // Item that the script applies to 
        public GameObject item;


    // Checks which item in the invenvtory is chosen, it matches and adds one to it
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().weapons.Length; i++)
            {
                if (GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().weapons[i] == item)
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().inventory[i]++;

            }

            //Destroys the object after its picked up
                Destroy(gameObject);

        }
    }
}
