using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class LogParser
{
    public static void Test()
    {
        string path = Application.dataPath;       
        path += "/../";

        Debug.Log("Exe path: " + path);

        string [] fileEntries = Directory.GetFiles(path);
        foreach (string fileName in fileEntries)
            Debug.Log(fileName);

        
    }
}



