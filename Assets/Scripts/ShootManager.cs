using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour
{
    [HideInInspector]
    public bool m_canShoot = true;
    public GameObject m_bullet;
    public bool Shoot(ColorType.ColorType.Type m_type)
    {
        if(m_canShoot)
        {
            m_canShoot = false;
            if(m_type == ColorType.ColorType.Type.White || m_type == ColorType.ColorType.Type.Red || m_type == ColorType.ColorType.Type.Orange || m_type == ColorType.ColorType.Type.Tangerine || m_type == ColorType.ColorType.Type.Yellow)
            {
                GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
                newBullet.transform.Rotate(0,0,Random.Range(-ColorType.ColorType.m_associatedSpread[(int)m_type], ColorType.ColorType.m_associatedSpread[(int)m_type]));
                Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
                
            }
            else if (m_type == ColorType.ColorType.Type.Green || m_type == ColorType.ColorType.Type.Lime)
            {
                GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
                newBullet.transform.Rotate(0,0,Random.Range(-ColorType.ColorType.m_associatedSpread[(int)m_type], ColorType.ColorType.m_associatedSpread[(int)m_type]));
                Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
                newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.right * newBullet.GetComponent<BulletBehaviour>().m_bulletSpeed, ForceMode2D.Impulse);
            }
            else if(m_type == ColorType.ColorType.Type.Navy || m_type == ColorType.ColorType.Type.Blue || m_type == ColorType.ColorType.Type.Cyan)
            {
                GameObject[] newBullet = new GameObject[5];
                Rigidbody2D newBulletRb;
                for(int i=0; i<5; i++)
                {   
                    newBullet[i] = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
                    newBullet[i].transform.Rotate(0,0,Random.Range(-ColorType.ColorType.m_associatedSpread[(int)m_type], ColorType.ColorType.m_associatedSpread[(int)m_type]));
                    newBulletRb = newBullet[i].GetComponent<Rigidbody2D>();
                    Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet[i].GetComponent<BoxCollider2D>());
                    for(int j=0; j<i ;j++)
                    {
                        Physics2D.IgnoreCollision(newBullet[j].GetComponent<BoxCollider2D>(),newBullet[i].GetComponent<BoxCollider2D>());
                    }
                }
            }
            StartCoroutine(FireCoolDown(m_type));
            return true;
        }
        return false;
    }

    IEnumerator FireCoolDown(ColorType.ColorType.Type m_type)
    {
        yield return new WaitForSeconds(ColorType.ColorType.m_associatedFireRate[(int)m_type]);
        m_canShoot = true;
    }
}
