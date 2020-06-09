using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float m_bulletSpeed = 50f;

    public GameObject m_fireLight;
    public GameObject m_impactLight;
    private TrailRenderer m_trail;
    void Start()
    {
        Instantiate(m_fireLight, this.transform.position, this.transform.rotation);
        m_trail = GetComponent<TrailRenderer>();
        StartCoroutine(StartTrail());
        GetComponent<Rigidbody2D>().AddForce(this.transform.right * m_bulletSpeed, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collider)
    {
        Instantiate(m_impactLight, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
    IEnumerator StartTrail()
    {
        yield return new WaitForSeconds(0.04f);
        m_trail.enabled = true;
    }
}
