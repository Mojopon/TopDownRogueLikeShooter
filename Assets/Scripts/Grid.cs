using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    public bool drawGridOnScene;

    public MapGenerator mapGenerator;
    public Transform player;
    public Transform target;

    public LayerMask unwalkableMask;
    public float nodeRadius;

    private Node[,] grid;
    private float nodeDiameter;
    private Vector2 originalMapSize;
    private int gridSizeX, gridSizeY;
    private Pathfinding pathfinding;

    public void Initialize()
    {
        if(mapGenerator == null)
        {
            Debug.Log("Map Generator has to be set!");
            return;
        }

        nodeDiameter = nodeRadius * 2;

        originalMapSize = mapGenerator.mapSize * mapGenerator.tileSize;
        Vector2 gridSize = originalMapSize / nodeDiameter;

        gridSizeX = Mathf.RoundToInt(gridSize.x);
        gridSizeY = Mathf.RoundToInt(gridSize.y);
        CreateGrid();

        pathfinding = GetComponent<Pathfinding>();
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = (transform.position - Vector3.right * originalMapSize.x / 2 - Vector3.up * originalMapSize.y / 2);
        Debug.Log(worldBottomLeft);
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> GetNeightbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;
                
                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = worldPosition.x / originalMapSize.x + 0.5f;
        float percentY = worldPosition.y / originalMapSize.y + 0.5f;

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        if (!drawGridOnScene || mapGenerator == null) return;

        //pathfinding.FindPath(player.position, target.position);
        //var path = pathfinding.Path;

        Gizmos.DrawWireCube(transform.position, new Vector3(originalMapSize.x, originalMapSize.y, 1));
        if(grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                //if (path.Contains(n)) Gizmos.color = Color.black;
                Gizmos.DrawCube(n.worldPosition - Vector3.forward, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
