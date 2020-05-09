
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float m_bulletSpeed = 50f;
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(this.transform.right * m_bulletSpeed, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        Destroy(this.gameObject);
    }
}
