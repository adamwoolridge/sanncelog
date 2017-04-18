﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UICameraTimeline : MonoBehaviour
{
    public Transform PrefabTimeBlock;
    public Transform PrefabGraphSeries;

    public Text TextChannelName;    

    public Transform TimeBlockParent;
    private int channel;

    private WMG_Series graphSeries;

    public void Init(int ch, string channelName, WMG_Axis_Graph graph)
    {     
        channel = ch;
        TextChannelName.text = channel + ". " + channelName;        

        for (int i=0; i<24; i++)
        {
            Transform tb = Instantiate(PrefabTimeBlock);
            tb.SetParent(TimeBlockParent, false);
        }

        graphSeries = graph.addSeries();
    }

    public void Show(DateTime date)
    {
        List<Vector2> seriesData = new List<Vector2>();

        List<LogMotionEntry> entries = LogParser.GetDailyLogs(date.Date);

        int i=0;

        foreach (Transform t in TimeBlockParent)
        {
            t.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            t.GetComponent<UITimeBlock>().ClearTriggerCount();
            seriesData.Add(new Vector2(i, 0));
            i++;
        }

        if (entries != null)
        {            
            foreach (LogMotionEntry entry in entries)
            {
                if (entry.channel == channel)
                {
                    int index = entry.start.Hour;

                    TimeBlockParent.GetChild(index).transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    Vector2 sd = seriesData[index];
                    sd.y = TimeBlockParent.GetChild(index).GetComponent<UITimeBlock>().IncreaseTriggerCount();
                    seriesData[index] = sd;                    
                }               
            }
        }

        graphSeries.pointValues.SetList(seriesData);
    }
}
