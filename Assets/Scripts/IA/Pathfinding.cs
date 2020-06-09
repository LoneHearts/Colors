using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Pathfinding : MonoBehaviour
{

    Grid m_grid;

    void Awake()
    {
        m_grid = GetComponent<Grid>();
    }


    public void FindPath(PathRequest request, Action<PathResult> callback)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = m_grid.NodeFromWorldPoint(request.pathStart);
        Node targetNode = m_grid.NodeFromWorldPoint(request.pathEnd);
        
        
        if (startNode.m_walkable && targetNode.m_walkable)
        {
            
            Heap<Node> openSet = new Heap<Node>(m_grid.MaxSize);
            
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {

                Node currentNode = openSet.RemoveFirst();


                closedSet.Add(currentNode);

                if(currentNode == targetNode)
                {
                    pathSuccess = true;
                    break;
                }

                foreach (Node neighbour in m_grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.m_walkable || closedSet.Contains(neighbour))
                    {
                        continue;
                    }

                    int newMovementCostToNeighbour = currentNode.m_gCost + GetDistanceNode(currentNode, neighbour) + neighbour.m_movementPenalty;
                    
                    if(newMovementCostToNeighbour < neighbour.m_gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.m_gCost = newMovementCostToNeighbour;
                        neighbour.m_hCost = GetDistanceNode(neighbour, targetNode);
                        neighbour.m_parent = currentNode;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else
                        {
                            openSet.UpdateItem(neighbour);
                        }
                    }
                }
            }
        }
        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
            pathSuccess = waypoints.Length > 0;
        }
        callback(new PathResult(waypoints,pathSuccess,request.callback));
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.m_parent;
        }
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
        

    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;
        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i-1].m_gridX - path[i].m_gridX, path[i-1].m_gridY - path[i].m_gridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(path[i].m_worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    } 


    int GetDistanceNode(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.m_gridX - nodeB.m_gridX);
        int dstY = Mathf.Abs(nodeA.m_gridY - nodeB.m_gridY);
    
        if (dstX > dstY)
        {
            return 14*dstY + 10*(dstX-dstY); 
        }
        return 14*dstX + 10*(dstY-dstX);
    }

}
