using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    LevelManager managerScript;
    C4 c4Script;

    // Left and right move speed;
        public float moveSpeed;
    // Height of jump
        public float jumpForce;
    // Checks if player is on the ground
        public bool isGrounded;
    // bool for left facing
        public bool isLeft;
    // Empty game object that holds the location of the weapon on the player
        public GameObject[] weaponSpawn;
    // Creates inventory
         GameObject[] inventory = new GameObject[2];
    // current inventory slot;
         int slot;
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        managerScript = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelManager>();

        inventory[0] = managerScript.weapons[0];
        inventory[1] = managerScript.weapons[1];
        slot = 0; 
        isGrounded = true;

        
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
            if (Input.mousePosition.x < transform.position.x)
            {
                isLeft = true;
                managerScript.inventory[slot]--;
                Instantiate(inventory[slot], weaponSpawn[0].transform.position, Quaternion.identity);
            }
            else
            {
                isLeft = false;
                managerScript.inventory[slot]--;
                Instantiate(inventory[slot], weaponSpawn[1].transform.position, Quaternion.identity);
            }
        }
           

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
            isGrounded = false;
    }
}
