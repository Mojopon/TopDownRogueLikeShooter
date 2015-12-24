using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Transform wallPrefab;
    public float tileSize = 1f;
    public Vector2 mapSize;
    public Vector2 maxRoomSize;
    public Vector2 minRoomSize;
    public MapCreationStrategyType strategy;


    void Start()
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

        IMap map = mapCreationStrategy.Create(mapSize, maxRoomSize, minRoomSize, 1);
        int[,] mapData = map.MapData;

        for (int x = 0; x < mapData.GetLength(0); x++)
        {
            for(int y = 0; y < mapData.GetLength(1); y++)
            {
                if (mapData[x, y] == 1)
                {
                    SpawnTile(x, y, TileType.Floor, mapHolder);
                }
                else if(mapData[x, y] == 2)
                {
                    SpawnTile(x, y, TileType.Wall, mapHolder);
                }
            }
        }
        Debug.Log("map generated");
    }

    void SpawnTile(int x, int y, TileType type, Transform mapHolder)
    {
        Transform tileToSpawn = null;
        Vector3 spawnPosition = CoordToPosition(x, y);

        switch(type)
        {
            case TileType.Floor:
                tileToSpawn = tilePrefab;
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

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-mapSize.x / 2f + 0.5f + x, -mapSize.y / 2f + 0.5f + y, 0) * tileSize;
    }  
}