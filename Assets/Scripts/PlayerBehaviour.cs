using UnityEngine;
using System.Collections;
using ColorType;

public class PlayerBehaviour : MonoBehaviour
{
    public float m_moveSpeed = 5f;
    private Rigidbody2D m_myRigidbody;
    private Camera m_worldCamera;
    private Vector2 m_movement;
    private Vector2 m_mousePosition;
    
    private SpriteRenderer m_sprite;
    
    private bool m_canShoot = true;

    private PlayerColor m_color;
    public GameObject m_bullet;

    

    void Start()
    {
        m_myRigidbody = GetComponent<Rigidbody2D>();
        m_worldCamera = Camera.main;
        m_sprite = GetComponent<SpriteRenderer>();
        m_color = GetComponent<PlayerColor>();
    }



    void Update()
    {
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_mousePosition = m_worldCamera.ScreenToWorldPoint(Input.mousePosition);

        Move();
        Aim();

        if(m_canShoot)
        {
            if(Input.GetButtonDown("Fire1") && !m_color.m_automatic)
            {
                Shoot();
            }
            else if (Input.GetButton("Fire1") && m_color.m_automatic)
            {
                Shoot();
            }  
        }
    }


    private void OnCollisionEnter2D(Collision2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            StealColor(collider.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Enemy")
        {
            StealColor(collider.gameObject);
            collider.gameObject.GetComponent<EnemyBehaviour>().Delete();
        }
    }


    private void Move()
    {
        m_myRigidbody.MovePosition(m_myRigidbody.position + m_movement * m_moveSpeed * Time.fixedDeltaTime);
    }

    private void Aim()
    {
        Vector2 lookDirection = m_mousePosition - m_myRigidbody.position;
        m_myRigidbody.rotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }

    private void Shoot()
    {
        m_canShoot = false;
        if(m_sprite.color == Color.white)
        {
            GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
            Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
            newBulletRb.AddForce(newBullet.transform.right * 10f, ForceMode2D.Impulse);
            
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
                newBulletRb.AddForce(newBullet[i].transform.right * Random.Range(8f,12f), ForceMode2D.Impulse);
            }
        }
        else if(m_sprite.color == Color.blue)
        {
            GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
            Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
            newBulletRb.AddForce(newBullet.transform.right * 10f, ForceMode2D.Impulse);
        }
        StartCoroutine(FireCoolDown());
    }

    private void StealColor(GameObject enemy)
    {
        m_color.ChangeColor(enemy.GetComponent<EnemyBehaviour>().m_type);
        enemy.GetComponent<EnemyBehaviour>().ChangeColor(ColorType.ColorType.Type.White);
        m_canShoot = true;
    }

    IEnumerator FireCoolDown()
    {
        yield return new WaitForSeconds(m_color.m_fireRate);
        m_canShoot = true;
    }

    
}
