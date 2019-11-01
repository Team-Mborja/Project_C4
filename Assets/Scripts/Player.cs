using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Creates Rigidbody2D
        Rigidbody2D rb;
    // Acceses the level mangaer
        LevelManager managerScript;
    // Left and right move speed;
        public float moveSpeed;
    // Height of jump
        public float jumpForce;
    // bool for left facing
        public bool isLeft;
        public int leftScale;
    // Empty game object that holds the location of the weapon on the player
        Vector2 weaponSpawn = Vector2.zero;
    // Creates inventory
         GameObject[] inventory = new GameObject[3];
    // current inventory slot;
         public int slot;


    public int[] usedEquipment = new int[3];
    public int usedJump;

    RaycastHit2D left;
    RaycastHit2D right;

    RaycastHit2D downLeft;
    RaycastHit2D downRight;


    // Start is called before the first frame update
    void Start()
    {
        // Gets the Rigidbody2D on the player
            rb = GetComponent<Rigidbody2D>();
        // Gets the script on the Level Manager
            managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();
        // Equips weapons from the level manager
            inventory[0] = managerScript.weapons[0];
            inventory[1] = managerScript.weapons[1];
            inventory[2] = managerScript.weapons[2];
        // Defaults to the first inventory slot
            slot = 0; 
        // Defaults to facing right
            isLeft = false;
            leftScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        left = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
        right = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);

        downLeft = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y), Vector2.down, 1.0f);
        downRight = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), Vector2.down, 1.0f);

        if (GameObject.FindGameObjectWithTag("FuseBox") != null && managerScript.pausedGame == false)
        {



            // Player moves right with D
            if (right.collider == null && Input.GetKey(KeyCode.D))
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            // Player moves left with A
            if (left.collider == null && Input.GetKey(KeyCode.A))
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

       
             if ((downLeft.collider != null || downRight.collider != null) && (Input.GetKeyDown(KeyCode.Space)))
            {
                rb.AddForce(Vector2.up * jumpForce);
                usedJump += 1;
            }

            // Weapon swap keys
            if (Input.GetKey(KeyCode.Alpha1))
                slot = 0;
            else if (Input.GetKey(KeyCode.Alpha2))
                slot = 1;
            else if (Input.GetKey(KeyCode.Alpha3))
                slot = 2;

            // Throws current weapon
            if (Input.GetMouseButtonDown(0) && managerScript.inventory[slot] > 0)
            {
                SpawnEqipment(inventory[slot]);
                usedEquipment[slot] += 1;
            }

            // Cheks to see what direction the player is facing
            if (Input.mousePosition.x >= Camera.main.WorldToScreenPoint(transform.position).x)
            {
                isLeft = false;
                leftScale = 1;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                isLeft = true;
                leftScale = -1;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }


    //Spawns each equipment at a given instantiation point
    void SpawnEqipment(GameObject equipment)
    {
        managerScript.inventory[slot]--;

        if (equipment.GetComponent<Grenade>() != null)
            Instantiate(equipment, new Vector2(transform.position.x + (equipment.GetComponent<Grenade>().offset.x * leftScale), transform.position.y + equipment.GetComponent<Grenade>().offset.y), Quaternion.identity);
        else if (equipment.GetComponent<C4>() != null)
            Instantiate(equipment, new Vector2(transform.position.x + (equipment.GetComponent<C4>().offset.x * leftScale) , transform.position.y + equipment.GetComponent<C4>().offset.y), Quaternion.identity);
        else if (equipment.GetComponent<Rocket_Launcher>() != null)
            Instantiate(equipment, new Vector2(transform.position.x + (equipment.GetComponent<Rocket_Launcher>().offset.x * leftScale), transform.position.y + equipment.GetComponent<Rocket_Launcher>().offset.y), Quaternion.Euler(0, 0, 90));

    }
}
