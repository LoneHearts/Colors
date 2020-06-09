using System.Collections;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float m_time = 0.5f;
    void Start()
    {
        StartCoroutine(ADestroy());
    }

    IEnumerator ADestroy()
    {
        yield return new WaitForSeconds(m_time);
        Destroy(this.gameObject);
    }
}
