using UnityEngine;
using System.Collections;

public abstract class GridTestFixture
{
    protected GridImpl grid;
    protected Node[,] nodes;

    protected int mapSizeX;
    protected int mapSizeY;

    protected void SetupTestFixture(int[,] map)
    {
        int mapSizeX = map.GetLength(1);
        int mapSizeY = map.GetLength(0);

        nodes = new Node[mapSizeX, mapSizeY];
        grid = new GridImpl(mapSizeX, mapSizeY);

        for (int y = 0; y < mapSizeY; y++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                nodes[x, y] = grid.CreateNode(x, y, map[y, x] == 0);
            }
        }
    }
}
