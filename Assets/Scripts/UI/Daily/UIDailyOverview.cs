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

	// Use this for initialization
	void Start () {
        LogParser.ParseAll();

        currentDay = DateTime.Now.Date;

        int index = 0;
        foreach (UICameraTimeline ct in CameraTimeLines)
        {
            ct.Init(index + 1, LogParser.channelNames[index]);            
            index++;
        }

        ShowDay(currentDay);
    }

    public void ShowDay(DateTime date)
    {
        TextCurrentDate.text = date.ToString("dd / MM / yyyy");

        foreach (UICameraTimeline ct in CameraTimeLines)
        {
            ct.Show(currentDay);
        }
    }

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentDay = currentDay.Date.AddDays(-1);
            ShowDay(currentDay);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentDay = currentDay.Date.AddDays(1);
            ShowDay(currentDay);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentDay = DateTime.Now.Date;
            ShowDay(currentDay);
        }
    }
}
