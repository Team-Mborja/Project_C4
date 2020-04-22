using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    // Position of the player
        Vector3 playerPos;
    // Offset for the camera from the player
    public float offset;
    // Is the camera bound
        public bool bounds;
    // Boundry positions for the camera
        public Vector3 minCameraPos;
        public Vector3 maxcameraPos;


    void Update()
    {
        // Keeps the camera at -10 on the z axis
            transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
        // updates the Player position every frame
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        // Updates the camera position as long as the player is alive
        if(GameObject.FindGameObjectWithTag("Player") != null)
            transform.position = new Vector3(playerPos.x, playerPos.y, transform.position.z);
    }

    //Lock the camera in place if the player reaches a boundry
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
