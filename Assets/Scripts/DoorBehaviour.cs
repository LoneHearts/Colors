using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{

    private Rigidbody2D m_body;
    private HingeJoint2D m_hinge;

    public float m_bouncy = 3f;
    void Start () 
    {
        m_hinge = GetComponent<HingeJoint2D>();
        m_body = gameObject.transform.GetChild(0).GetComponent<Rigidbody2D>();
    }
    
    void Update () 
    {
        JumpBack();
    }

    void JumpBack()
    {
        if (m_hinge.jointAngle >= m_hinge.limits.max-10 && m_body.angularVelocity>0)
        {
            m_body.velocity = -m_body.velocity/m_bouncy;
            m_body.angularVelocity = -m_body.angularVelocity/m_bouncy;
        }
        else if(m_hinge.jointAngle > m_hinge.limits.max)
        {
            m_body.angularVelocity = - m_bouncy * 50;
        }
        if (m_hinge.jointAngle <= m_hinge.limits.min+10 && m_body.angularVelocity<0)
        {
            m_body.velocity = -m_body.velocity/m_bouncy;
            m_body.angularVelocity = -m_body.angularVelocity/m_bouncy;
        }
        else if(m_hinge.jointAngle < m_hinge.limits.min)
        {
            m_body.angularVelocity = m_bouncy * 50;
        }
    }

    
}