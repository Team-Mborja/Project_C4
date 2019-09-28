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
    // Checks if player is on the ground
        public bool isGrounded;
    // bool for left facing
        public bool isLeft;
    // Empty game object that holds the location of the weapon on the player
        public GameObject weaponSpawn;
    // Creates inventory
         GameObject[] inventory = new GameObject[2];
    // current inventory slot;
         int slot;
   

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
        // Defaults to the first inventory slot
            slot = 0; 
        // Defaults to not being on the ground
            isGrounded = true;
        // Defaults to facing right
            isLeft = false;

    }

    // Update is called once per frame
    void Update()
    {
        // Player moves right with D and right arrow
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                transform.Translate(Vector2.right* moveSpeed);

        // Player moves left with A and left arrow
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                transform.Translate(Vector2.left * moveSpeed);

        // Player jumps with W and up arrow and space
            if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
                rb.AddForce(Vector2.up * jumpForce);

        // Weapon swap keys
            if (Input.GetKey(KeyCode.Alpha1)) 
                slot = 0;
            else if (Input.GetKey(KeyCode.Alpha2))
                slot = 1;

        // Throws current weapon
        if (Input.GetMouseButtonDown(0) && managerScript.inventory[slot] > 0)
        {
                managerScript.inventory[slot]--;
                Instantiate(inventory[slot], weaponSpawn.transform.position, Quaternion.identity); 
        }

        // Cheks to see what direction the player is facing
            if (Input.mousePosition.x >= Camera.main.WorldToScreenPoint(transform.position).x)
            {
                isLeft = false;
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                isLeft = true;
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

    }

    // Detects if player is on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            isGrounded = true;
    }

    //Detects if player is off the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            isGrounded = false;
    }

  
}
