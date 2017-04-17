using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICameraTimeline : MonoBehaviour
{
    public Text TextChannelName;
    public Transform TimeBlockParent;	

    public void Init(int channel, string channelName)
    {
        TextChannelName.text = channel + ". " + channelName;
    }
}
