using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour, IGrid
{
    public bool drawGridOnScene;

    public MapGenerator mapGenerator;
    public Transform player;
    public Transform target;

    public LayerMask unwalkableMask;
    public float nodeRadius;
    public float nodeOverlapRate = 0.5f;

    private GridImpl grid;
    private float nodeDiameter;
    private Vector2 originalMapSize;
    private int gridSizeX, gridSizeY;
    private Pathfinding pathfinding;

    public Node this[int x, int y]
    {
        get { return grid[x, y]; }
    }

    public List<Node> GetNeightbours(Node node)
    {
        return grid.GetNeightbours(node);
    }

    public Node GetClosestAvailableNode(Node targetNode)
    {
        return grid.GetClosestAvailableNode(targetNode);
    }

    public int MaxSize { get { return grid.MaxSize; } }
    public int Width { get { return grid.Width; } }
    public int Height { get { return grid.Height; } }


    public void Initialize()
    {
        if(mapGenerator == null)
        {
            Debug.Log("Map Generator has to be set!");
            return;
        }

        nodeDiameter = nodeRadius * 2;
        IMap map = mapGenerator.CurrentMap;
        originalMapSize = map.MapSize * map.TileSize;
        Vector2 gridSize = originalMapSize / nodeDiameter;

        gridSizeX = Mathf.RoundToInt(gridSize.x);
        gridSizeY = Mathf.RoundToInt(gridSize.y);
        CreateGrid();

        pathfinding = GetComponent<Pathfinding>();
    }

    void CreateGrid()
    {
        grid = new GridImpl(gridSizeX, gridSizeY);
        Vector3 worldBottomLeft = (transform.position - Vector3.right * originalMapSize.x / 2 - Vector3.up * originalMapSize.y / 2);
        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, nodeRadius * nodeOverlapRate, unwalkableMask));
                grid.CreateNode(x, y, walkable, worldPoint);
            }
        }
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
