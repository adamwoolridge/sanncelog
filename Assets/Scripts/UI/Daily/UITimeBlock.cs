using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UITimeBlock : MonoBehaviour, IPointerEnterHandler
{
    public Text TextTriggerCount;
    public Image ImageAlert;

    int triggerCount = 0;

    public List<LogMotionEntry> AlertEntries;

    public int IncreaseTriggerCount(Color textColor)
    {
        triggerCount++;
        TextTriggerCount.color = textColor;
        TextTriggerCount.text = triggerCount.ToString();        
        return triggerCount;
    }	
    
    public void Reset()
    {
        AlertEntries = new List<LogMotionEntry>();
        triggerCount = 0;
        TextTriggerCount.text = "";
        ImageAlert.enabled = false;
    }		

    public void AddAlert(LogMotionEntry entry)
    {
        ImageAlert.enabled = true;

        AlertEntries.Add(entry);
    }

    public void OnPointerEnter(PointerEventData pd)
    {
        if (AlertEntries == null || AlertEntries.Count == 0)
            return;

        foreach (LogMotionEntry entry in AlertEntries)
        {
            Debug.Log(entry.channel + ". " + entry.start + " (" + entry.duration + "s )");
        }
        Debug.Log(AlertEntries.Count);
    }
}
