using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class PathfindTest : GridTestFixture
{
    int[,] map = new int[,]
    {
        { 0,1,0,0,0 },
        { 0,1,0,1,0 },
        { 0,1,0,1,0 },
        { 0,1,0,1,0 },
        { 0,0,0,1,0 },
    };

    int[,] map2 = new int[,]
    {
        { 0,0,0,0,0,0 },
        { 1,1,1,1,1,0 },
        { 0,0,0,0,0,0 },
        { 0,1,1,1,1,1 },
        { 0,0,0,0,0,0 },
    };

    int[,] map3 = new int[,]
    {
        { 0,1,0,0,0 },
        { 0,1,0,1,0 },
        { 0,1,0,1,0 },
        { 0,1,0,1,1 },
        { 0,0,0,1,0 },
    };

    [Test]
    public void ShouldFindPath()
    {
        SetupTestFixture(map);

        var pathfinder = new PathfindingImpl(grid);

        var startNode = nodes[0, 0];
        var targetNode = nodes[4, 4];

        var path = pathfinder.FindPath(startNode, targetNode);

        // ensure last node in the list is the first next step to the target node
        Assert.AreEqual(nodes[0, 1], path[path.Count - 1]);
        path.Reverse();

        Assert.AreEqual(12, path.Count);

        Assert.AreEqual(nodes[0, 1], path[0]);
        Assert.AreEqual(nodes[0, 2], path[1]);
        Assert.AreEqual(nodes[0, 3], path[2]);
        Assert.AreEqual(nodes[1, 4], path[3]);
        Assert.AreEqual(nodes[2, 3], path[4]);
        Assert.AreEqual(nodes[2, 2], path[5]);
        Assert.AreEqual(nodes[2, 1], path[6]);
        Assert.AreEqual(nodes[3, 0], path[7]);
        Assert.AreEqual(nodes[4, 1], path[8]);
        Assert.AreEqual(nodes[4, 2], path[9]);
        Assert.AreEqual(nodes[4, 3], path[10]);
        Assert.AreEqual(nodes[4, 4], path[11]);

        //{ 0,1,0,0,0 }
        //{ 0,1,0,1,0 }
        //{ 0,1,0,1,0 }
        //{ 0,1,0,1,0 }
        //{ 0,0,0,1,0 }
    }

    [Test]
    public void PathFindTest2()
    {

        SetupTestFixture(map2);

        var pathfinder = new PathfindingImpl(grid);

        var startNode = nodes[0, 0];
        var targetNode = nodes[5, 4];

        var path = pathfinder.FindPath(startNode, targetNode);
        path.Reverse();

        Assert.AreEqual(15, path.Count);

        Assert.AreEqual(nodes[1, 0], path[0]);
        Assert.AreEqual(nodes[2, 0], path[1]);
        Assert.AreEqual(nodes[3, 0], path[2]);
        Assert.AreEqual(nodes[4, 0], path[3]);
        Assert.AreEqual(nodes[5, 1], path[4]);
        Assert.AreEqual(nodes[4, 2], path[5]);
        Assert.AreEqual(nodes[3, 2], path[6]);
        Assert.AreEqual(nodes[2, 2], path[7]);
        Assert.AreEqual(nodes[1, 2], path[8]);
        Assert.AreEqual(nodes[0, 3], path[9]);
        Assert.AreEqual(nodes[1, 4], path[10]);
        Assert.AreEqual(nodes[2, 4], path[11]);
        Assert.AreEqual(nodes[3, 4], path[12]);
        Assert.AreEqual(nodes[4, 4], path[13]);
        Assert.AreEqual(nodes[5, 4], path[14]);


        //{ 0,0,0,0,0,0 }
        //{ 1,1,1,1,1,0 }
        //{ 0,0,0,0,0,0 }
        //{ 0,1,1,1,1,1 }
        //{ 0,0,0,0,0,0 }

    }

    [Test]
    public void ShouldReturnBlankListWhenNoPathAvailable()
    {
        SetupTestFixture(map3);

        var pathfinder = new PathfindingImpl(grid);

        var startNode = nodes[0, 0];
        var targetNode = nodes[4, 4];

        var path = pathfinder.FindPath(startNode, targetNode);

        Assert.AreEqual(0, path.Count);
    }
}
