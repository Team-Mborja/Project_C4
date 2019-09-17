using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    // Left and right move speed;
        public float moveSpeed;
    // Height of jump
        public float jumpForce;
    // Checks if player is on the ground
    public bool isGrounded;

    // Holds the current weapon of the player
        GameObject currentWeapon;
    // Empty game object that holds the location of the weapon on the player
        public GameObject weaponSpawn;
    // Weapon in first slot
        public GameObject weapon1;
    // Weapon in seconds slot
        public GameObject weapon2;


    // inventory of every type of explosive for the level
        float inventory1;
        float inventory2;
        float inventory3;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        isGrounded = true;
        currentWeapon = weapon1;
    }

    // Update is called once per frame
    void Update()
    {
        // Player moves right with D and right arrow
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            rb.AddForce(Vector2.right * moveSpeed);

        // Player moves left with A and left arrow
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            rb.AddForce(Vector2.left * moveSpeed);
        
        // Player jumps with W and up arrow and space
        if (isGrounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)))
            rb.AddForce(Vector2.up * jumpForce);

        // Weapon swap keys
        if (Input.GetKey(KeyCode.Alpha1))
            currentWeapon = weapon1;
        else if (Input.GetKey(KeyCode.Alpha2))
            currentWeapon = weapon2;

        // Throws current weapon
        if (Input.GetMouseButtonDown(0))
            Instantiate(currentWeapon, weaponSpawn.transform.position, Quaternion.identity);

        // Detonates C4
        if (Input.GetMouseButtonDown(1) && currentWeapon.tag  == "C4")
        {
            GameObject[] c4s = GameObject.FindGameObjectsWithTag("C4");
            foreach (GameObject c4 in c4s)
                GameObject.Destroy(c4);
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
