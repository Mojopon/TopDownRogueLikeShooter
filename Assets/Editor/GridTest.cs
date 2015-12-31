using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GridTest : GridTestFixture
{
    int[,] map = new int[,]
    {
            { 1,1,0,0,0 },
            { 0,1,0,1,0 },
            { 0,1,0,1,0 },
            { 0,1,0,1,0 },
            { 0,0,0,1,0 },
    };

    [SetUp]
    public void SetUp()
    {
        gridSizeX = 10;
        gridSizeY = 5;

        nodes = new Node[gridSizeX, gridSizeY];
        grid = new GridImpl(gridSizeX, gridSizeY);
    }

    [Test]
    public void ShouldCreateNode()
    {
        var node = grid.CreateNode(3, 4, true);
        Assert.IsTrue(node.gridX == 3 && node.gridY == 4);
    }

    [Test]
    public void CanReferenceNode()
    {
        var node = grid.CreateNode(1, 2, true);
        Assert.AreEqual(grid[1, 2], node);
    }

    [Test]
    public void ShouldReturnWidthAndHeight()
    {
        Assert.AreEqual(gridSizeX, grid.Width);
        Assert.AreEqual(gridSizeY, grid.Height);
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

    [Test]
    public void ShouldGetClosestAvailableNode()
    {
        SetupTestFixture(map);

        Assert.IsFalse(grid[0, 0].walkable);
        var closestAvailableNode = grid.GetClosestAvailableNode(grid[0, 0]);
        Assert.AreEqual(grid[0, 1], closestAvailableNode);
    }

    public void SetupGrid()
    {
        for (int y = 0; y < gridSizeY; y++)
        {
            for(int x = 0; x < gridSizeX; x++)
            {
                nodes[x, y] = grid.CreateNode(x, y, true);
            }
        }
    }
}
