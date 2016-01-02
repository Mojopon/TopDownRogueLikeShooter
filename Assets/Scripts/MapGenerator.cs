using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

[RequireComponent(typeof(MapCreationStrategy))]
public class MapGenerator : MonoBehaviour
{
    public MapCreationStrategy mapCreationStrategy;
    public Transform tilePrefab;
    public Transform wallPrefab;
    public float tileSize = 1f;

    public MapCreationStrategyType strategy;

    [HideInInspector]
    public List<Vector3> AvailablePositions;

    public IMap CurrentMap;

    public void Initialize()
    {
        GenerateMap();
    }
        
    public void GenerateMap()
    {
        string holderName = "Generated Map";
        if(transform.FindChild(holderName))
        {
            DestroyImmediate(transform.FindChild(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        var newMap = mapCreationStrategy.Create();
        newMap.TileSize = tileSize;
        CurrentMap = newMap;
        TileType[,] tileData = CurrentMap.TileData;

        AvailablePositions = new List<Vector3>();

        for (int x = 0; x < tileData.GetLength(0); x++)
        {
            for (int y = 0; y < tileData.GetLength(1); y++)
            {
                SpawnTile(x, y, tileData[x, y], mapHolder);
            }
        }
    }

    void SpawnTile(int x, int y, TileType type, Transform mapHolder)
    {
        if (type == TileType.None) return;

        Transform tileToSpawn = null;
        Vector3 spawnPosition = CurrentMap.CoordToPosition(x, y);

        switch(type)
        {
            case TileType.Floor:
                tileToSpawn = tilePrefab;
                AvailablePositions.Add(spawnPosition);
                spawnPosition += new Vector3(0, 0, 1);
                break;
            case TileType.Wall:
                tileToSpawn = wallPrefab;
                break;
        }

        Transform newTile = Instantiate(tileToSpawn, spawnPosition, Quaternion.identity) as Transform;
        newTile.localScale = Vector3.one * tileSize;
        newTile.parent = mapHolder;
    }

    void UpdateGraph()
    {
        if(AstarPath.active.astarData.gridGraph == null)
        {
            Debug.Log("grid graph is null");
            return;
        }

        float nodeSize = tileSize /2;
        float width = CurrentMap.Width / (nodeSize/2);
        float depth = CurrentMap.Height / (nodeSize/2);

        Debug.Log(string.Format("Node Size: {0}, Width: {1}, Depth: {2},", nodeSize, width, depth));

        GridGraph gridGraph = AstarPath.active.astarData.gridGraph;
        gridGraph.Width = Mathf.RoundToInt(width);
        gridGraph.Depth = Mathf.RoundToInt(depth);
        gridGraph.center = new Vector3(0, 0.1f, 0);
        AstarPath.active.Scan();

    }

    void OnDrawGizmos()
    {
        var mapCreationStrategy = GetComponent<MapCreationStrategy>();
        Gizmos.DrawWireCube(transform.position, new Vector3(mapCreationStrategy.mapSize.x, mapCreationStrategy.mapSize.y, 1) * tileSize);
    }
}