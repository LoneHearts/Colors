using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Camera m_camera;
    public Transform m_target;
    public float m_smoothSpeed = 0.10f;
    void Start()
    {
        this.m_camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 target_position = m_target.position + new Vector3(0,0,-10);
        m_camera.transform.position = Vector3.Lerp(this.transform.position, target_position, m_smoothSpeed);
    }
}
