using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDeath : MonoBehaviour
{
    private PlayerBehaviour m_player;
    void Start()
    {
        LevelManager.Instance.player.SetDeathUI(this);
    }
    
    public void Enable()
    {
        Text[] texts = GetComponentsInChildren<Text>();
        foreach(Text text in texts)
        {
            text.enabled = true;
        }
    }
}


