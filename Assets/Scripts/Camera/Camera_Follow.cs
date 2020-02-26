using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    private Transform playerTransform;

    public float offset;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxcameraPos;

    void Start () {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    //called after update and fixed update
    void LateUpdate()
    {
        if (playerTransform != null)
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

        }
    }
    void fixedupdate()
    {
        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxcameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxcameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxcameraPos.z));
        }
    }

}
