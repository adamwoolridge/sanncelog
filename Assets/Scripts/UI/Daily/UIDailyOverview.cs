using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDailyOverview : MonoBehaviour {

    public UICameraTimeline [] CameraTimeLines;

	// Use this for initialization
	void Start () {
        LogParser.ParseAll();

        int index = 0;
        foreach (UICameraTimeline ct in CameraTimeLines)
        {
            ct.Init(index + 1, LogParser.channelNames[index]);
            index++;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
