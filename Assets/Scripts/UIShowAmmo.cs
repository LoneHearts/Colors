using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowAmmo : MonoBehaviour
{
    private Text m_text;
    private PlayerBehaviour m_player;
    
    void Awake()
    {
        m_text = GetComponent<Text>();
        LevelManager.Instance.player.SetAmmoUi(this);
    }

    public void UpdateAmmo(int ammo)
    {
        m_text.text = ammo.ToString();
    }

    public void UpdateAmmo(int ammo, Color newColor)
    {
        m_text.text = ammo.ToString();
        m_text.color = newColor;
    }
}
