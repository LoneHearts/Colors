using System.Collections;
using UnityEngine;

public class Node : IHeapItem<Node>
{
    public bool m_walkable;
    public Vector3 m_worldPosition;
    public int m_gridX;
    public int m_gridY;
    public int m_movementPenalty;

    public int m_gCost;
    public int m_hCost;
    public Node m_parent;
    int heapIndex;
    public Node(bool walkable, Vector3 worldPosition, int gridX, int gridY, int penalty)
    {
        m_walkable = walkable;
        m_worldPosition = worldPosition;
        m_gridX = gridX;
        m_gridY = gridY;
        m_movementPenalty = penalty;
    }

    public int fCost
    {
        get
        {
            return m_gCost + m_hCost;
        }
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = m_hCost.CompareTo(nodeToCompare.m_hCost);
        }
        return -compare;
    }
}
