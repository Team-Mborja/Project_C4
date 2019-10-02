using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
   public GameObject item;


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            for (int i = 0; i < GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().weapons.Length; i++)
            {
                if (GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().weapons[i] == item)
                    GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>().inventory[i]++;

            }

            Destroy(gameObject);

        }
    }
}
