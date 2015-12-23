using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour
{
    public Transform tilePrefab;
    public Vector2 mapSize;
    public MapCreationStrategyType strategy;


    void Start()
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

        for(int x = 0; x < mapSize.x; x++)
        {
            for(int y = 0; y < mapSize.y; y++)
            {
                Vector3 tilePosition = new Vector3(-mapSize.x / 2 + 0.5f + x, -mapSize.y / 2 + 0.5f + y, 1);
                Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity) as Transform;
                newTile.parent = mapHolder;
            }
        }
    }
}
