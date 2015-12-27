using UnityEngine;
using System.Collections;

public interface IMap
{
    Vector2 MapSize { get; }
    float TileSize { get; }
    TileType[,] TileData { get; }

    Vector3 CoordToPosition(int x, int y);
}
