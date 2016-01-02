using UnityEngine;
using System.Collections;

[System.Serializable]
public class Map : IMap
{
    public Vector2 MapSize { get; set; }
    public int Width { get { return TileData.GetLength(0); } }
    public int Height { get { return TileData.GetLength(1); } }
    public float TileSize { get; set; }
    public TileType[,] TileData { get; set; }

    public Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-MapSize.x / 2f + 0.5f + x, -MapSize.y / 2f + 0.5f + y, 0) * TileSize;
    }
}
