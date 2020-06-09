using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
public class PathRequestManager : MonoBehaviour
{
    Queue<PathResult> m_results = new Queue<PathResult>();
    static PathRequestManager m_instance;
    Pathfinding m_pathfinding;

    void Awake()
    {
        m_instance = this;
        m_pathfinding = GetComponent<Pathfinding>();
    }

    void Update()
    {
        if(m_results.Count>0)
        {
            int itemsInQueue = m_results.Count;
            lock(m_results)
            {
                for(int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = m_results.Dequeue();
                    result.callback(result.path,result.success);
                }
            }
        }
    }

    public static void RequestPath(PathRequest request)
    {
        ThreadStart threadStart = delegate {
            m_instance.m_pathfinding.FindPath(request, m_instance.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }

    public void FinishedProcessingPath(PathResult result)
    {
        lock(m_results)
        {
            m_results.Enqueue(result);
        }
    }

    
    
}

public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> back)
    {
        pathStart = start;
        pathEnd = end;
        callback = back;
    }
}

public struct PathResult
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }
}