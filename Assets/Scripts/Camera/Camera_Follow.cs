using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    Vector3 playerPos;
    public float offset;

    public bool bounds;

    public Vector3 minCameraPos;
    public Vector3 maxcameraPos;


    void Update()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
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
