using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorType;

public class EnemyBehaviour : MonoBehaviour
{
    private CircleCollider2D m_collider;
    private SpriteRenderer m_sprite;
    public ColorType.ColorType.Type m_type;
    void Start()
    {
        m_collider = GetComponent<CircleCollider2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        ChangeColor(m_type);
    }

    public void ChangeColor(ColorType.ColorType.Type newColor)
    {
        m_sprite.color = ColorType.ColorType.m_associatedColor[(int)newColor];
        m_type = newColor;
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            Die();
        }
    }

    private void Die()
    {
        //Can slowly decrease power when dead -> less ammunition
        m_sprite.color = new Color(m_sprite.color.r,m_sprite.color.g,m_sprite.color.b,m_sprite.color.a/2);
        m_collider.isTrigger = true;
        m_sprite.sortingOrder--;
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}
