using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsChange : MonoBehaviour
{
    // Text field for the key name and key
        public Text keyName;
        public Text key;
    // Button to change key binding
        public Button changeButton;
    // Index of which key you are chnaging
        public int index;
    // Script of the settings manager
        SettingsManager settingsScript;
    // Bool for weather or not you can chnage the key binding 
        public bool canChange;
    // Keycode that you are changing it to
        KeyCode changeKey;

    // Start is called before the first frame update
    void Start()
    {
        // Defaults canChnage to false
            canChange = false;
        // Assignes settings script
            settingsScript = GameObject.FindGameObjectWithTag("Settings").GetComponent<SettingsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sets key text to its correct control
            key.text = settingsScript.controls[index];

        // Changes key only if button is pressed
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

    // Sets canChange to true when you press the button
    public void ChangeKey()
    {
        canChange = true;
    }
}
