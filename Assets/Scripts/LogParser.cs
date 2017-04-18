using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;
using System.Globalization;

public static class LogParser
{
    public static Dictionary<int, LogMotionEntry> openChannelLogs;
    public static Dictionary<int, List<LogMotionEntry>> channelLogs;
    public static Dictionary<DateTime, List<LogMotionEntry>> dailyLogs;
    public static List<LogMotionEntry> combinedLogs;

    public static string [] channelNames =
    {
        "Driveway",
        "Front Street",
        "Front Door",
        "Back Garden",
    };

    public static void ParseAll()
    {
        string path = Application.dataPath;
        path += "/../";

        Debug.Log("Exe path: " + path);

        string[] files = System.IO.Directory.GetFiles(path, "*.txt");

        openChannelLogs = new Dictionary<int, LogMotionEntry>();
        channelLogs = new Dictionary<int, List<LogMotionEntry>>();
        dailyLogs = new Dictionary<DateTime, List<LogMotionEntry>>();
        combinedLogs = new List<LogMotionEntry>();

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
                        lme.start = DateTime.ParseExact(splitStrings[1], "dd-MM-yyyy HH:mm:ss", null);
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
                        lme.end = DateTime.ParseExact(splitStrings[1], "dd-MM-yyyy HH:mm:ss", null);
                        lme.duration = (lme.end - lme.start).Seconds;
                        openChannelLogs.Remove(channel);
                    }

                    if (lme != null)
                    {
                        List<LogMotionEntry> channelEntries;

                        if (!channelLogs.TryGetValue(channel, out channelEntries))
                        {
                            channelEntries = new List<LogMotionEntry>();
                            channelLogs.Add(channel, channelEntries);
                        }

                        channelEntries.Add(lme);
                        combinedLogs.Add(lme);

                        List<LogMotionEntry> dailyList;

                        if (!dailyLogs.TryGetValue(lme.start.Date, out dailyList))
                        {
                            dailyList = new List<LogMotionEntry>();
                            dailyLogs.Add(lme.start.Date, dailyList);
                        }

                        dailyList.Add(lme);
                    }
                }
            }
        }

        foreach (KeyValuePair<int, List<LogMotionEntry>> channelLog in channelLogs)
        {
            Debug.Log(channelNames[channelLog.Key-1] + " (" + channelLog.Key + "): " + channelLog.Value.Count + " log entries");
        }

        if (combinedLogs.Count > 0)
        { 
            Debug.Log("First entry: " + combinedLogs[0].start);
            Debug.Log("Last entry: " + combinedLogs[combinedLogs.Count - 1].start);
        }
    }

    public static List<LogMotionEntry> GetDailyLogs(DateTime date)
    {
        List<LogMotionEntry> entries = null;

        dailyLogs.TryGetValue(date, out entries);

        return entries;            
    }

}



