using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeBlock : MonoBehaviour
{
    public Text TextTriggerCount;

    int triggerCount = 0;
    
    public int IncreaseTriggerCount()
    {
        triggerCount++;
        TextTriggerCount.text = triggerCount.ToString();
        return triggerCount;
    }	
    
    public void ClearTriggerCount()
    {
        triggerCount = 0;
        TextTriggerCount.text = "";
    }		
}
