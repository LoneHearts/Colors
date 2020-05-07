using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private SpriteRenderer m_color;

    public enum ColorType{Blue,Cyan,Green,Magenta,Red,Yellow,Black,Grey,White};
    private Color[] m_associatedColor = {Color.blue, Color.cyan, Color.green, Color.magenta, Color.red, Color.yellow, Color.black, Color.grey, Color.white};
    public ColorType m_type;
    void Start()
    {
        m_color = GetComponent<SpriteRenderer>();
        ChangeColor(m_associatedColor[(int)m_type]);
    }

    public void ChangeColor(Color newColor)
    {
        m_color.color = newColor;
    }
}
