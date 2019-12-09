using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    // Makes the mouse turn on and creates the audio manager if necessary
    void Awake()
    {
        Cursor.visible = true;
    }

    // Restarts the level when you press R
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            RestartScene();
    }

    // Restarts the current level
    public void RestartScene()
    {
        OpenScene(SceneManager.GetActiveScene().name);
    }

    // Opens a scene
    public void OpenScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    // Closes the game
    public void CloseGame()
    {
        Application.Quit();
    }

}
