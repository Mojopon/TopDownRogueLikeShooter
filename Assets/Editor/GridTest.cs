using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GridTest
{
    Node[,] nodes;
    GridImpl grid;
    int gridSizeX, gridSizeY;
    [SetUp]
    public void SetUp()
    {
        gridSizeX = 10;
        gridSizeY = 5;

        nodes = new Node[gridSizeX, gridSizeY];
        grid = new GridImpl(gridSizeX, gridSizeY);
    }

    [Test]
    public void CanSetandGetGrid()
    {
        Node node = new Node(true, Vector3.zero, 3, 3);
        grid[0, 0] = node;
        Assert.AreEqual(node, grid[0, 0]);
    }

    [Test]
    public void ShouldGetNeighboursOfTargettedNode()
    {
        SetupGrid();
        int targetX, targetY;
        targetX = 1;
        targetY = 1;
        var target = grid[targetX, targetY];

        var neighbours = grid.GetNeightbours(target);
        for (int y = -1; y < 1; y++)
        {
            for (int x = -1; x < 1; x++)
            {
                if (x == 0 && y == 0) continue;
                Assert.IsTrue(neighbours.Contains(nodes[targetX + x, targetY + y]));
            }
        }

        neighbours = grid.GetNeightbours(nodes[0, 0]);
        Assert.IsTrue(neighbours.Contains(nodes[0, 1]));
        Assert.IsTrue(neighbours.Contains(nodes[1, 1]));
        Assert.IsTrue(neighbours.Contains(nodes[1, 0]));
        Assert.IsFalse(neighbours.Contains(nodes[0, 0]));

    }

    [Test]
    public void ShouldReturnNumberOfNodes()
    {
        Assert.AreEqual(gridSizeX * gridSizeY, grid.MaxSize);
    }

    public void SetupGrid()
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            for(int x = 0; x < gridSizeX; x++)
            {
                nodes[x, y] = new Node(true, Vector3.zero, x, y);
                grid[x, y] = nodes[x, y];
            }
        }
    }
}
