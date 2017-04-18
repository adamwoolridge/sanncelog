using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UITimeSpanHeader : MonoBehaviour
{
    public Transform PrefabTimeHeaderBlock;    
    public Transform TimeBlockParent;
    
    public void Start()
    {        
        for (int i=0; i<24; i++)
        {
            Transform tb = Instantiate(PrefabTimeHeaderBlock);
            tb.SetParent(TimeBlockParent, false);
            tb.GetComponent<UITimeSpanHeaderBlock>().TextTime.text = i.ToString("00") + ":00";
        }
    }   
}
