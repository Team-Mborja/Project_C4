using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{


    private Transform playerTransform;

    public float offset;
    public Animation anim;


    IEnumerator wait()
    {
        // The wait function in the IEnumerator
        yield return new WaitForSeconds(5);
    }
    void Start () {


        anim = GetComponent<Animation>();
                    //anim.Play("YourAnimationClipName"); // <--- Change this to the actual name of the animation




        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    //called every frame
    void Update()
    {

    }

    //called every fixed frame rate
    void FixedUpdate()
    {
        
    }

    //called after update and fixed update
    void LateUpdate()
    {
        // we store current camera's positon in variable temporary
        Vector3 temp = transform.position;

        //we set the players camera's position equal to the player's position x
        temp.x = playerTransform.position.x;

        //temp.y = playerTransform.position.y;
        //temp.y += offset;


        //this will add the offset value to the temp camera pos
        temp.x += offset;

        //we set back the camera's temp pos to the camera's current pos
        transform.position = temp;

        StartCoroutine(wait()); //testing
    }


}
