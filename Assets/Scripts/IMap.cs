using UnityEngine;
using System.Collections;

public interface IMap
{
    TileType[,] TileData { get; }
}
