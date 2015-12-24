using UnityEngine;
using System.Collections;

[System.Serializable]
public class Map : IMap
{
    public TileType[,] MapData { get; set; }
}
