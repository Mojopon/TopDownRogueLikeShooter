using UnityEngine;
using System.Collections;

public abstract class GridTestFixture
{
    protected GridImpl grid;
    protected Node[,] nodes;

    protected int gridSizeX;
    protected int gridSizeY;

    protected void SetupTestFixture(int[,] map)
    {
        gridSizeX = map.GetLength(1);
        gridSizeY = map.GetLength(0);

        nodes = new Node[gridSizeX, gridSizeY];
        grid = new GridImpl(gridSizeX, gridSizeY);

        for (int y = 0; y < gridSizeY; y++)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                nodes[x, y] = grid.CreateNode(x, y, map[y, x] == 0);
            }
        }
    }
}
