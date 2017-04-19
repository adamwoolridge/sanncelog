using System.Collections;
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

    public Color [] Colours;

    public void Init(int ch, string channelName, WMG_Axis_Graph graph)
    {     
        channel = ch;
        TextChannelName.text = channel + ". " + channelName;
        TextChannelName.color = Colours[ch - 1];

        for (int i=0; i<24; i++)
        {
            Transform tb = Instantiate(PrefabTimeBlock);
            tb.SetParent(TimeBlockParent, false);
        }

        graphSeries = graph.addSeries();
        graphSeries.seriesName = channelName;
        graphSeries.lineColor = Colours[ch - 1];
    }

    public int Show(DateTime date)
    {
        List<Vector2> seriesData = new List<Vector2>();

        List<LogMotionEntry> entries = LogParser.GetDailyLogs(date.Date);

        int i=0;

        foreach (Transform t in TimeBlockParent)
        {
            t.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            t.GetComponent<UITimeBlock>().Reset();
            seriesData.Add(new Vector2(i, 0));
            i++;
        }

        int highest = 0;

        if (entries != null)
        {            
            foreach (LogMotionEntry entry in entries)
            {
                if (entry.channel == channel)
                {
                    int index = entry.start.Hour;

                    UITimeBlock timeBLock = TimeBlockParent.GetChild(index).GetComponent<UITimeBlock>();

                    timeBLock.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    Vector2 sd = seriesData[index];
                    sd.y = timeBLock.IncreaseTriggerCount(Colours[channel-1]);                    
                    seriesData[index] = sd;

                    if ((int)sd.y > highest)
                        highest = (int)sd.y;

                    if (entry.duration > 5)
                    {
                        timeBLock.AddAlert(entry);
                    }
                }               
            }
        }

        graphSeries.UseXDistBetweenToSpace = true;
        graphSeries.pointValues.SetList(seriesData);

        return highest;
    }
}
