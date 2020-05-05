
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collider)
    {
        Destroy(this.gameObject);
    }
}
