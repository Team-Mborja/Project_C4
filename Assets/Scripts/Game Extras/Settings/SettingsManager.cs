using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SettingsManager : MonoBehaviour
{
    public string path;
    string defaultFile = "User Controls\n\nA\nD\nSpace\nAlpha1\nAlpha2\nAlpha3";
    public List<string> controls = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        CreateFile("Controls", defaultFile);
        ReadFile(path);
    }


    void CreateFile(string fileName, string contents)
    {
        path = Application.dataPath + "/" + fileName + ".txt";

        if (!File.Exists(path))
            File.WriteAllText(path, contents);
        
    }

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

    public void UpdateFile(string fileName)
    {
        string fileText = "User Controls\n";

        for(int i = 0; i < controls.Count; i++)
        {
            fileText += "\n" + controls[i];
        }

        CreateFile(path, fileText);
        ReadFile(path);
    }
}
