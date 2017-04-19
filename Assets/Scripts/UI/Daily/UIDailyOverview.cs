using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class UIDailyOverview : MonoBehaviour {

    public UICameraTimeline [] CameraTimeLines;
    public Text TextCurrentDate;
    private DateTime currentDay;
    public WMG_Axis_Graph Graph;

	// Use this for initialization
	void Start () {
        LogParser.ParseAll();

        currentDay = DateTime.Now.Date;

        int index = 0;
        foreach (UICameraTimeline ct in CameraTimeLines)
        {
            ct.Init(index + 1, LogParser.channelNames[index], Graph);            
            index++;
        }

        ShowDay(currentDay);
    }

    public void ShowDay(DateTime date)
    {
        TextCurrentDate.text = date.ToString("dd / MM / yyyy");

        int highest = 0;

        foreach (UICameraTimeline ct in CameraTimeLines)
        {
            int high = ct.Show(currentDay);

            if (high > highest)
                highest = high;
        }

        Graph.yAxis.AxisMaxValue = highest > 0 ? highest : 10;        
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentDay = currentDay.Date.AddDays(-1);
            ShowDay(currentDay);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentDay = currentDay.Date.AddDays(1);
            ShowDay(currentDay);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetMouseButtonDown(2))
        {
            currentDay = DateTime.Now.Date;
            ShowDay(currentDay);
        }
    }
}
