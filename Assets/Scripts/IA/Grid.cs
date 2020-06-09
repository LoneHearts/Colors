using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public bool m_displayGridGizmos = false;
    public LayerMask m_unwalkableMask;
    public Vector2 m_gridWorldSize;
    public float m_nodeRadius;
    public TerrainType[] m_walkableRegion;

    public int m_obstacleProximityPenalty;
    LayerMask m_walkableMask;
    Dictionary<int,int> m_walkableRegionDictionary = new Dictionary<int, int>();

    Node[,] m_grid;

    
    float m_nodeDiameter;
    int m_gridSizeX, m_gridSizeY;

    int m_penaltyMin = int.MaxValue;
    int m_penaltyMax = int.MinValue;

    void Awake()
    {
        m_nodeDiameter = m_nodeRadius*2;
        m_gridSizeX = Mathf.RoundToInt(m_gridWorldSize.x/m_nodeDiameter);
        m_gridSizeY = Mathf.RoundToInt(m_gridWorldSize.y/m_nodeDiameter);

        foreach(TerrainType region in m_walkableRegion)
        {
            m_walkableMask.value |= region.terrainMask.value;
            m_walkableRegionDictionary.Add((int)Mathf.Log(region.terrainMask.value,2),region.terrainPenalty);
        }

        CreateGrid();
    }

    public int MaxSize
    {
        get
        {
            return m_gridSizeX * m_gridSizeY;
        }
    }

    void CreateGrid()
    {
        m_grid = new Node[m_gridSizeX,m_gridSizeY];
        Vector3 worldBottomLeft = transform.position - (Vector3.right*m_gridWorldSize.x/2) - (Vector3.up*m_gridWorldSize.y/2); 

        for(int x = 0; x< m_gridSizeX; x++)
        {
            for(int y = 0; y< m_gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x*m_nodeDiameter + m_nodeRadius) + Vector3.up * (y*m_nodeDiameter + m_nodeRadius);
                Vector2 box = new Vector2(m_nodeDiameter - 0.1f, m_nodeDiameter - 0.1f);
                bool walkable = !(Physics2D.OverlapBox(worldPoint, box, 90, m_unwalkableMask));
                int movementPenalty = 0;

                foreach(TerrainType region in m_walkableRegion)
                {
                    if(Physics2D.OverlapBox(worldPoint, box, 90, region.terrainMask))
                    {
                        movementPenalty = region.terrainPenalty;
                    }
                }
                
                if(!walkable)
                {
                    movementPenalty += m_obstacleProximityPenalty;
                }

                m_grid[x,y] = new Node(walkable,worldPoint,x,y,movementPenalty);
            }
        }
        BlurPenaltyMap(5);
    }

    void BlurPenaltyMap(int blurSize)
    {
        int kernelSize = blurSize * 2 + 1;
        int kernelExtents = (kernelSize-1) /2;

        int[,] penaltiesHorizontalPass = new int[m_gridSizeX,m_gridSizeY];
        int[,] penaltiesVerticalPass = new int[m_gridSizeX,m_gridSizeY];

        for(int y = 0; y< m_gridSizeY; y++)
        {
            for(int x = -kernelExtents; x <= kernelExtents; x++)
            {
                int sampleX = Mathf.Clamp(x, 0, kernelExtents);
                penaltiesHorizontalPass[0,y] += m_grid[sampleX,y].m_movementPenalty;
            }
            for(int x = 1; x < m_gridSizeX; x++)
            {
                int removeIndex = Mathf.Clamp(x - kernelExtents -1, 0, m_gridSizeX);
                int addIndex = Mathf.Clamp(x + kernelExtents, 0, m_gridSizeX-1);
                penaltiesHorizontalPass[x,y] = penaltiesHorizontalPass[x-1, y] - m_grid[removeIndex, y].m_movementPenalty + m_grid[addIndex, y].m_movementPenalty;
            }
        }

        for(int x = 0; x< m_gridSizeX; x++)
        {
            for(int y = -kernelExtents; y <= kernelExtents; y++)
            {
                int sampleY = Mathf.Clamp(y, 0, kernelExtents);
                penaltiesVerticalPass[x,0] += penaltiesHorizontalPass[x, sampleY];
            }

            int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x,0] / (kernelSize*kernelSize));
            m_grid[x,0].m_movementPenalty = blurredPenalty;

            for(int y = 1; y < m_gridSizeY; y++)
            {
                int removeIndex = Mathf.Clamp(y - kernelExtents -1, 0, m_gridSizeY);
                int addIndex = Mathf.Clamp(y + kernelExtents, 0, m_gridSizeY-1);
               
                penaltiesVerticalPass[x,y] = penaltiesVerticalPass[x, y-1] - penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
                blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x,y] / (kernelSize*kernelSize));
                m_grid[x,y].m_movementPenalty = blurredPenalty;

                if(blurredPenalty > m_penaltyMax)
                {
                    m_penaltyMax = blurredPenalty;
                }
                if(blurredPenalty < m_penaltyMin)
                {
                    m_penaltyMin = blurredPenalty;
                }
            }
        }

    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        for(int x=-1; x <= 1; x++)
        {
            for(int y=-1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.m_gridX + x;
                int checkY = node.m_gridY + y;

                if(checkX >= 0 && checkX < m_gridSizeX && checkY >= 0 && checkY < m_gridSizeY)
                {
                    neighbours.Add(m_grid[checkX,checkY]);
                }
            }
        }
        return neighbours;
    }
    public Node NodeFromWorldPoint(Vector3 m_worldPosition)
    {
        float percentX = (m_worldPosition.x + m_gridWorldSize.x/2) / m_gridWorldSize.x;
        float percentY = (m_worldPosition.y + m_gridWorldSize.y/2) / m_gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((m_gridSizeX-1) * percentX);
        int y = Mathf.RoundToInt((m_gridSizeY-1) * percentY);
        return m_grid[x,y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(m_gridWorldSize.x,m_gridWorldSize.y,1));
    
        
        if (m_grid != null && m_displayGridGizmos)
        {
            foreach (Node n in m_grid)
            {

                Gizmos.color = Color.Lerp(Color.white,Color.black, Mathf.InverseLerp(m_penaltyMin,m_penaltyMax,n.m_movementPenalty));

                Gizmos.color = (n.m_walkable)?Gizmos.color:Color.red;
                Gizmos.DrawCube(n.m_worldPosition, Vector3.one * (m_nodeDiameter));
            }
        }
        
    }

    [System.Serializable]
    public class TerrainType{
        public LayerMask terrainMask;
        public int terrainPenalty;
    }


}
