using UnityEngine;
using System.Collections;

[System.Serializable]
public class Map : IMap
{
    public TileType[,] TileData { get; set; }
}
