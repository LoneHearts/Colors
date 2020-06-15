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
            if(m_type == ColorType.ColorType.Type.White)
            {
                GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
                Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
                
            }
            else if(m_type == ColorType.ColorType.Type.Blue)
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
            else if(m_type == ColorType.ColorType.Type.Red)
            {
                GameObject newBullet = Instantiate(m_bullet, this.transform.position, this.transform.rotation);
                Rigidbody2D newBulletRb = newBullet.GetComponent<Rigidbody2D>();
                Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(),newBullet.GetComponent<BoxCollider2D>());
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
