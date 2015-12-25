using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public Transform player;

    public LayerMask unwalkableMask;
    public float nodeRadius;

    private Node[,] grid;
    private float nodeDiameter;
    private Vector2 originalMapSize;
    private int gridSizeX, gridSizeY;



    void Start()
    {
        if(mapGenerator == null)
        {
            Debug.Log("Map Generator has to be set!");
            return;
        }

        originalMapSize = mapGenerator.mapSize * mapGenerator.tileSize;
        Vector2 gridSize = mapGenerator.mapSize / nodeRadius;

        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridSize.x);
        gridSizeY = Mathf.RoundToInt(gridSize.y);
        CreateGrid();
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
                grid[x, y] = new Node(walkable, worldPoint);
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
        if (mapGenerator == null) return;

        Gizmos.DrawWireCube(transform.position, new Vector3(originalMapSize.x, originalMapSize.y, 1));
        if(grid != null)
        {
            Node playerNode = NodeFromWorldPoint(player.position);
            foreach(Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                if(playerNode == n)
                {
                    Gizmos.color = Color.cyan;
                }
                Gizmos.DrawCube(n.worldPosition - Vector3.forward, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
