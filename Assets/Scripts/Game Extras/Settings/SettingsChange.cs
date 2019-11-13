using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsChange : MonoBehaviour
{
    public Text keyName;
    public Text key;
    public Button changeButton;
    public int index;
    SettingsManager settingsScript;

    public bool canChange;
    KeyCode changeKey;

    // Start is called before the first frame update
    void Start()
    {
        canChange = false;
        settingsScript = GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        key.text = settingsScript.controls[index];

        foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode) && canChange == true)
            {
                settingsScript.controls[index] = kcode.ToString();
                canChange = false;
                settingsScript.UpdateFile(settingsScript.path);
            }
        }
    }

    public void ChangeKey()
    {
        canChange = true;
    }
}
