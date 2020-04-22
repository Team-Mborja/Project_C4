using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    // String of file path
        public string path;
    // Default controls written to the file
        string defaultFile = "User Controls\n\nA\nD\nSpace\nAlpha1\nAlpha2\nAlpha3";
    // Controls of the player
        public List<string> controls = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        CreateFile("Controls", defaultFile);
        ReadFile(path);
    }

    // Creates a text file if one does't exist
    void CreateFile(string fileName, string contents)
    {
        path = Application.dataPath + "/" + fileName + ".txt";

        if (!File.Exists(path))
            File.WriteAllText(path, contents);
        
    }

    // Sets the controls to what contents are in the file
    void ReadFile(string fileName)
    {
        StreamReader reader = new StreamReader(path);
        string line;

        controls.Clear();

        while((line = reader.ReadLine()) != null)
        {
            if(line != "User Controls" && line != "")
                controls.Add(line);
        }
       

    }

    // Updates the text file with new controls
    public void UpdateFile(string fileName)
    {
        string fileText = "User Controls\n";

        for(int i = 0; i < controls.Count; i++)
        {
            fileText += "\n" + controls[i];
        }

        File.WriteAllText(path, fileText);
        ReadFile(path);
    }
}
