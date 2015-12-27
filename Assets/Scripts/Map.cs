using UnityEngine;
using System.Collections;

[System.Serializable]
public class Map : IMap
{
    public Vector2 MapSize { get; set; }
    public float TileSize { get; set; }
    public TileType[,] TileData { get; set; }
}
