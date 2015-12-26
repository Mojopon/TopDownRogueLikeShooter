using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform wallPrefab;
    public float tileSize = 1f;
    public Vector2 mapSize;
    public Vector2 maxRoomSize;
    public Vector2 minRoomSize;
    public int seed;

    public MapCreationStrategyType strategy;

    public List<Vector3> AvailablePositions;

    private IMap currentMap;

    public void Initialize()
    {
        GenerateMap();
    }
        
    public void GenerateMap()
    {
        var mapCreationStrategy = new BasicMapCreationStrategy();

        string holderName = "Generated Map";
        if(transform.FindChild(holderName))
        {
            DestroyImmediate(transform.FindChild(holderName).gameObject);
        }

        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        currentMap = mapCreationStrategy.Create(mapSize, maxRoomSize, minRoomSize, seed);
        TileType[,] tileData = currentMap.TileData;

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
        Vector3 spawnPosition = CoordToPosition(x, y);

        switch(type)
        {
            case TileType.Floor:
                tileToSpawn = tilePrefab;
                spawnPosition += new Vector3(0, 0, 1);
                AvailablePositions.Add(spawnPosition);
                break;
            case TileType.Wall:
                tileToSpawn = wallPrefab;
                break;
        }

        Transform newTile = Instantiate(tileToSpawn, spawnPosition, Quaternion.identity) as Transform;
        newTile.localScale = Vector3.one * tileSize;
        newTile.parent = mapHolder;
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2f + 0.5f + x, -mapSize.y / 2f + 0.5f + y, 0) * tileSize;
    }  
}