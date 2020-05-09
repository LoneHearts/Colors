using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorType;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyBehaviour : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Light2D m_light;
    private CircleCollider2D m_collider;
    private SpriteRenderer m_sprite;
    public ColorType.ColorType.Type m_type;

    private bool m_dead = false;
    void Start()
    {
        m_light = GetComponentInChildren<Light2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Bullet")
        {
            m_rigidbody.velocity = Vector2.zero;
        }
    }

    private void Die()
    {
        //Can slowly decrease power when dead -> less ammunition
        if(!m_dead)
        {
            m_dead = true;
            m_collider.isTrigger = true;
            m_light.color = ColorType.ColorType.m_associatedColor[(int)m_type];
            m_light.enabled = true;
            m_sprite.color = new Color(m_sprite.color.r,m_sprite.color.g,m_sprite.color.b,m_sprite.color.a/3);
            m_sprite.sortingOrder--;
        }
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    }
}