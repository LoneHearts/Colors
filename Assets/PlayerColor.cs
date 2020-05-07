using UnityEngine;
using ColorType;
public class PlayerColor : MonoBehaviour
{

    private SpriteRenderer m_sprite;

    [HideInInspector]
    public bool m_automatic = false;

    [HideInInspector]
    public float m_fireRate = 0.5f;



    void Start()
    {
        m_sprite = GetComponent<SpriteRenderer>();  
        UpdateWeapon();
    }


    public void ChangeColor(ColorType.ColorType.Type newColor)
    {
        m_sprite.color = ColorType.ColorType.m_associatedColor[(int)newColor];
        UpdateWeapon();
    }

    

    private void UpdateWeapon()
    {
        if (m_sprite.color == Color.blue)
        {
            m_automatic = true;
            m_fireRate = 0.1f;
        }
        else if (m_sprite.color == Color.cyan)
        {
            m_automatic = false;
            m_fireRate = 0.5f;
        }
        else if (m_sprite.color == Color.green)
        {
            m_automatic = false;
            m_fireRate = 0.5f;
        }
        else if (m_sprite.color == Color.magenta)
        {
            m_automatic = false;
            m_fireRate = 0.5f;
        }
        else if (m_sprite.color == Color.red)
        {
            m_automatic = false;
            m_fireRate = 0.25f;
        }
        else if (m_sprite.color == Color.yellow)
        {
            m_automatic = false;
            m_fireRate = 0.5f;
        }
        else if (m_sprite.color == Color.black)
        {
            m_automatic = false;
            m_fireRate = 0.5f;
        }
        else if (m_sprite.color == Color.grey)
        {
            m_automatic = false;
            m_fireRate = 0.5f;
        }
        else if (m_sprite.color == Color.white)
        {
            m_automatic = false;
            m_fireRate = 0.0f;
        }
    }
}
