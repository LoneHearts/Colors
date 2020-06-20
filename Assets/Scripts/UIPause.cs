using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    public void Enable()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        foreach(Text text in texts)
        {
            text.enabled = true;
        }
    }

    public void Disable()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        foreach(Text text in texts)
        {
            text.enabled = false;
        }
    }
}