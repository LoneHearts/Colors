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
        SceneManager.Instance.player.SetAmmoUi(this);
    }

    public void UpdateAmmo(int ammo)
    {
        m_text.text = ammo.ToString();
    }
}
