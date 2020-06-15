using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColorType;
using UnityEngine.Experimental.Rendering.Universal;

public class EnemyBehaviour : MonoBehaviour
{
    private Unit m_pathfinding;
    private bool m_hasSeenPlayer = false;
    private Rigidbody2D m_rigidbody;
    private Light2D m_light;
    private CircleCollider2D m_collider;
    private SpriteRenderer m_sprite;

    private RaycastHit2D m_lineOfSight;
    public ColorType.ColorType.Type m_type;

    private PlayerBehaviour m_player;

    private bool m_dead = false;
    private ShootManager m_shoot;
    void Start()
    {
        m_shoot = GetComponent<ShootManager>();
        m_pathfinding = GetComponent<Unit>();
        m_player = SceneManager.Instance.player;
        m_light = GetComponentInChildren<Light2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();
        m_sprite = GetComponent<SpriteRenderer>();
        ChangeColor(m_type);

    }

    void FixedUpdate()
    {
        if(!m_player.m_dead)
        {
            m_lineOfSight = Physics2D.Raycast(transform.position, m_player.transform.position-transform.position);
            if(m_lineOfSight.collider.gameObject.tag == "Player")
            {
                m_pathfinding.enabled = false;
                Vector3 vectorToTarget = m_player.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if(m_hasSeenPlayer)
                {
                    m_shoot.Shoot(m_type);
                }
                else
                {
                    StartCoroutine(FirstShootLatency());
                }
            }
            else if(m_hasSeenPlayer)
            {
                m_pathfinding.enabled = true;
            }
        }
    }

    IEnumerator FirstShootLatency()
    {
        yield return new WaitForSeconds(0.5f);
        m_shoot.Shoot(m_type);
        m_hasSeenPlayer = true;
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
            Die(collider.relativeVelocity);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag != "Bullet")
        {
            m_rigidbody.velocity = Vector2.zero;
        }
    }

    private void Die(Vector2 knockback)
    {
        //Can slowly decrease power when dead -> less ammunition
        if(!m_dead)
        {
            StopCoroutine(FirstShootLatency());
            SceneManager.Instance.EnemyKilled();
            m_pathfinding.enabled = false;
            this.enabled = false;
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