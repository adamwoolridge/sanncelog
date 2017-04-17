using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public static class LogParser
{
    public static void Test()
    {
        string path = Application.dataPath;       
        path += "/../";

        Debug.Log("Exe path: " + path);

        string[] files = System.IO.Directory.GetFiles(path, "*.txt");

        Dictionary<int, LogMotionEntry> openChannelLogs = new Dictionary<int, LogMotionEntry>();

        foreach (string fileName in files)
        {
            StreamReader reader = File.OpenText(fileName);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                List<string> splitStrings = line.Split(new[]{"  "}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

                if (splitStrings[2] == "Alarm Start")
                {
                    int channel = int.Parse((splitStrings[3])[splitStrings[3].Length-1].ToString());
                    LogMotionEntry lme;

                    if (!openChannelLogs.TryGetValue(channel, out lme))
                    {
                        lme = new LogMotionEntry();
                        lme.startTimeDate = splitStrings[1];
                        lme.channel = channel;
                        openChannelLogs.Add(channel, lme);
                    }
                }
                else if (splitStrings[2] == "Alarm Stop")
                {
                    int channel = int.Parse((splitStrings[3])[splitStrings[3].Length-1].ToString());
                    LogMotionEntry lme;

                    if (openChannelLogs.TryGetValue(channel, out lme))
                    {
                        lme.endTimeDate = splitStrings[1];
                        Debug.Log(lme.channel + ": " + lme.startTimeDate + "-" + lme.endTimeDate);
                    }
                    
                }
            }
        }
    }
}



