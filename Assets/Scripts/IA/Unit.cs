using System.Collections;
using UnityEngine;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = 0.2f;
    const float pathUpdateMoveThreshhold = 0.5f;
    public bool m_drawGizmos = true;
    public Transform m_target;
    public float m_speed = 7f;

    public float m_turnSpeed = 3f;
    public float m_turnDst = 5f;

    Path m_path;

    void OnDisable()
    {
        StopCoroutine("FollowPath");
    }

    void OnEnable()
    { 
        PathRequestManager.RequestPath(new PathRequest(transform.position, m_target.position,OnPathFound));
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if(pathSuccessful)
        {
            m_path = new Path(waypoints, transform.position, m_turnDst);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }


    IEnumerator FollowPath()
    {

        bool followingPath = true;
        int pathIndex = 0;
        transform.LookAt (m_path.lookPoints [0]);

        while(followingPath)
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);

            while (m_path.turnBoundaries[pathIndex].HasCrossedLine(pos2D))
            {
                if(pathIndex == m_path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                else
                {
                    pathIndex++;
                }
            }

            if(followingPath)
            {
                Vector3 vectorToTarget = m_path.lookPoints[pathIndex] - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.Translate(Vector3.right*Time.deltaTime*m_speed, Space.Self);
            }

            yield return null;
        }
    }

    public void OnDrawGizmos()
    {
        if (m_path != null && m_drawGizmos)
        {
           m_path.DrawWithGizmos();
        }
    }
}
