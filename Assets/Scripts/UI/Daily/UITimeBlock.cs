using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeBlock : MonoBehaviour
{
    public Text TextTriggerCount;
    public Image ImageAlert;

    int triggerCount = 0;
    
    public int IncreaseTriggerCount(Color textColor)
    {
        triggerCount++;
        TextTriggerCount.color = textColor;
        TextTriggerCount.text = triggerCount.ToString();        
        return triggerCount;
    }	
    
    public void Reset()
    {
        triggerCount = 0;
        TextTriggerCount.text = "";
        ImageAlert.enabled = false;
    }		

    public void ShowAlert()
    {
        ImageAlert.enabled = true;
    }
}
