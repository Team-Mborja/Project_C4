using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;

    public float moveSpeed;
    public float jumpForce;
    public float maximumSpeed;

    public bool isGrounded;

    GameObject currentWeapon;
    public GameObject weaponSpawn;
    public GameObject grenade;
    public GameObject c4;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = true;
        rb = GetComponent<Rigidbody2D>();
        currentWeapon = grenade;
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

        if (Input.GetKey(KeyCode.Alpha1))
            currentWeapon = grenade;
        else if (Input.GetKey(KeyCode.Alpha2))
            currentWeapon = c4;

        // Throws current weapon
        if (Input.GetMouseButtonDown(0))
            Instantiate(currentWeapon, weaponSpawn.transform.position, Quaternion.identity);

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
