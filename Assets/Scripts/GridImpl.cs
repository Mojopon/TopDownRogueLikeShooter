using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GridImpl : IGrid, IEnumerable
{
    Node[,] grid;

    int gridSizeX, gridSizeY;

    public Node this[int x, int y]
    {
        get { return grid[x, y]; }
    }

    public GridImpl(int _gridSizeX, int _gridSizeY)
    {
        grid = new Node[_gridSizeX, _gridSizeY];
        gridSizeX = _gridSizeX;
        gridSizeY = _gridSizeY;
    }

    public int MaxSize
    {
        get
        {
            return gridSizeX * gridSizeY;
        }
    }

    public int Width { get { return gridSizeX; } }
    public int Height { get { return gridSizeY; } }

    public List<Node> GetNeightbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    public Node CreateNode(int x, int y, bool walkable, Vector3 worldPosition)
    {
        grid[x, y] = new Node(walkable, worldPosition, x, y);
        return grid[x, y];
    }

    public Node CreateNode(int x, int y, bool walkable)
    {
        return CreateNode(x, y, walkable, Vector3.zero);
    }

    public Node GetClosestAvailableNode(Node targetNode)
    {
        if (targetNode.walkable) return targetNode;

        return null;
    }

    public IEnumerator GetEnumerator()
    {
        for(int y = 0; y < gridSizeY; y++)
        {
            for(int x = 0; x < gridSizeX; x++)
            {
                yield return grid[x, y];
            }
        }
    }
}
