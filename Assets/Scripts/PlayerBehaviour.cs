using UnityEngine;
using System.Collections;
using ColorType;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerBehaviour : MonoBehaviour
{
    public float m_moveSpeed = 5f;
    private Rigidbody2D m_myRigidbody;
    private Camera m_worldCamera;
    private Vector2 m_movement;
    private Vector2 m_mousePosition;
    
    private SpriteRenderer m_sprite;
    
    private bool m_canShoot = true;
    public GameObject m_bullet;

    private ColorType.ColorType.Type m_type;

    private Light2D m_halo;

    public int m_ammo;
    private UIShowAmmo m_uiAmmo;

    //private Vector2 m_stopPushing = Vector2.zero;

    

    void Start()
    {
        m_halo = transform.GetChild(0).gameObject.GetComponent<Light2D>();
        m_myRigidbody = GetComponent<Rigidbody2D>();
        m_worldCamera = Camera.main;
        m_sprite = GetComponent<SpriteRenderer>();
        m_uiAmmo.UpdateAmmo(m_ammo);
    }



    void FixedUpdate()
    {
        m_movement.x = Input.GetAxisRaw("Horizontal");
        m_movement.y = Input.GetAxisRaw("Vertical");

        m_mousePosition = m_worldCamera.ScreenToWorldPoint(Input.mousePosition);

        Move();
        Aim();

        
    }

    void Update()
    {
        if(m_canShoot && m_ammo > 0)
        {
            if(Input.GetButtonDown("Fire1") && !ColorType.ColorType.m_associatedAutomatic[(int)m_type])
            {
                Shoot();
            }
            else if (Input.GetButton("Fire1") && ColorType.ColorType.m_associatedAutomatic[(int)m_type])
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
        m_myRigidbody.velocity = Vector2.zero;
    }

    private void Aim()
    {
        Vector2 lookDirection = m_mousePosition - m_myRigidbody.position;
        m_myRigidbody.rotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
    }

    private void Shoot()
    {
        m_canShoot = false;
        m_ammo--;
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
        m_uiAmmo.UpdateAmmo(m_ammo);
        StartCoroutine(FireCoolDown());
    }

    private void StealColor(GameObject enemy)
    {
        ChangeColor(enemy.GetComponent<EnemyBehaviour>().m_type);
        enemy.GetComponent<EnemyBehaviour>().ChangeColor(ColorType.ColorType.Type.Black);
        m_canShoot = true;
    }

    public void ChangeColor(ColorType.ColorType.Type newColor)
    {
        m_sprite.color = ColorType.ColorType.m_associatedColor[(int)newColor];
        m_halo.color = ColorType.ColorType.m_associatedColor[(int)newColor];
        m_ammo = ColorType.ColorType.m_associatedAmmo[(int)newColor];
        m_uiAmmo.UpdateAmmo(m_ammo);
        m_type = newColor;
    }

    public void SetAmmoUi(UIShowAmmo ui)
    {
        m_uiAmmo = ui;
    }

    IEnumerator FireCoolDown()
    {
        yield return new WaitForSeconds(ColorType.ColorType.m_associatedFireRate[(int)m_type]);
        m_canShoot = true;
    }

    
}
