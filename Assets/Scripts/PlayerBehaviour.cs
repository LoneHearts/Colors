using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float m_moveSpeed = 5f;
    private Rigidbody2D m_myRigidbody;
    private Camera m_worldCamera;
    private Vector2 m_movement;
    private Vector2 m_mousePosition;
    
    private SpriteRenderer m_Sprite;

    private bool m_automatic = false;


    public GameObject m_bullet;

    void Start()
    {
        m_myRigidbody = GetComponent<Rigidbody2D>();
        m_worldCamera = Camera.main;
        m_Sprite = GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_mousePosition = m_worldCamera.ScreenToWorldPoint(Input.mousePosition);

        Move();
        Aim();

        if(Input.GetButtonDown("Fire1") && !m_automatic)
        {
            Shoot();
        }
        else if (Input.GetButton("Fire1") && m_automatic)
        {
            Shoot();
        }  

        if(Input.GetButtonDown("Fire2") && !m_automatic)
        {
            m_Sprite.color = Color.red;
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
        if(m_Sprite.color == Color.white)
        {
            GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
            Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
            newBulletRb.AddForce(newBullet.transform.right * 10f, ForceMode2D.Impulse);
        }
        else if(m_Sprite.color == Color.red)
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
                print(newBullet[i].transform.rotation);
                newBulletRb.AddForce(newBullet[i].transform.right * Random.Range(8f,12f), ForceMode2D.Impulse);
            }
        }
    }
}
