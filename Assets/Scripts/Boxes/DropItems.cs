using UnityEngine;

public class DropItems : MonoBehaviour
{
   // Item that you are picking up 
        public GameObject item;

    // If a player runs into the gameobject, add it to your inventory
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
