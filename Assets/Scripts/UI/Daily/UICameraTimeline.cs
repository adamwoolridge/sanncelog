using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UICameraTimeline : MonoBehaviour
{
    public Text TextChannelName;    

    public Transform TimeBlockParent;
    private int channel;

    public void Init(int ch, string channelName)
    {
        channel = ch;
        TextChannelName.text = channel + ". " + channelName;        
    }

    public void Show(DateTime date)
    {
        List<LogMotionEntry> entries = LogParser.GetDailyLogs(date.Date);

        foreach (Transform t in TimeBlockParent)
            t.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        if (entries != null)
        {
            foreach (LogMotionEntry entry in entries)
            {
                if (entry.channel == channel)
                {
                    TimeBlockParent.GetChild(entry.start.Hour).transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    TimeBlockParent.GetChild(entry.start.Hour).transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
            }
        }       
    }
}
