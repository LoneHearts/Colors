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

    private bool m_canShoot = true;

    public GameObject m_bullet;
    void Start()
    {
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
                if(m_canShoot && m_hasSeenPlayer)
                {
                    Shoot();
                }
                else if(m_canShoot)
                {
                    StartCoroutine(FirstShootLatency());
                }
                m_hasSeenPlayer = true;
            }
            else if(m_hasSeenPlayer)
            {
                m_pathfinding.enabled = true;
            }
        }
    }

    IEnumerator FirstShootLatency()
    {
        m_canShoot = false;
        yield return new WaitForSeconds(0.5f);
        Shoot();
    }

    private void Shoot()
    {
        m_canShoot = false;
        if(m_sprite.color == Color.white)
        {
            GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
            Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
            
        }
        else if(m_sprite.color == Color.red)
        {
            GameObject[] newBullet = new GameObject[5];
            Rigidbody2D newBulletRb;
            for(int i=0; i<5; i++)
            {   
                newBullet[i] = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
                newBullet[i].transform.Rotate(0,0,Random.Range(-10f,10f));
                newBulletRb = newBullet[i].GetComponent<Rigidbody2D>();
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet[i].GetComponent<BoxCollider2D>());
                for(int j=0; j<i ;j++)
                {
                    Physics2D.IgnoreCollision(newBullet[j].GetComponent<BoxCollider2D>(),newBullet[i].GetComponent<BoxCollider2D>());
                }
            }
        }
        else if(m_sprite.color == Color.blue)
        {
            GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
            Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
        }
        StartCoroutine(FireCoolDown());
    }

    IEnumerator FireCoolDown()
    {
        yield return new WaitForSeconds(ColorType.ColorType.m_associatedFireRate[(int)m_type]);
        m_canShoot = true;
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