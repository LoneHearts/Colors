using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISlowMo : MonoBehaviour
{
    private Slider m_slider;
    private Image m_sliderImage;
    private PlayerBehaviour m_player;
    
    void Start()
    {
        m_slider = GetComponent<Slider>();
        m_sliderImage = GetComponentInChildren<Image>();
        LevelManager.Instance.player.SetSlowMoUi(this);
    }

    public void UpdateValue(float value)
    {
        m_slider.value = value;
    }

    public void UpdateColor(Color newColor)
    {
        m_slider.value = 1f;
        m_sliderImage.color = newColor;
    }
}
