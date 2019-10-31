using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Awake()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
            RestartScene();
    }
    public void RestartScene()
    {
        OpenScene(SceneManager.GetActiveScene().name);
    }

    public void OpenScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
