using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;
using System.Linq;
using System.Diagnostics;

[TestFixture]
public class PathReferenceTest : GridTestFixture
{
    PathfindingImpl pathfinder;
    PathReference pathReference;

    int[,] map = new int[,]
    {
        { 0,1,0,0,0 },
        { 0,1,0,1,0 },
        { 0,1,0,1,0 },
        { 0,1,0,1,0 },
        { 0,0,0,1,0 },
    };

    int[,] map2 = new int[25, 25];

    //[SetUp]
    public void SetUp()
    {
        SetupTestFixture(map);

        pathfinder = new PathfindingImpl(grid);
        pathReference = new PathReference(grid, pathfinder);
    }

    //[Test]
    public void SpeedCheck()
    {
        SetupTestFixture(map2);

        pathfinder = new PathfindingImpl(grid);
        pathReference = new PathReference(grid, pathfinder);

        Stopwatch sw = new Stopwatch();
        sw.Start();

        for (int x = 0; x < 20; x++)
        {
            for(int y = 0; y < 20; y++)
            {
                pathReference.UpdatePathReference(x, y);
            }
        }

        sw.Stop();
        UnityEngine.Debug.Log("testing done: " + sw.ElapsedMilliseconds);
    }

    //[Test]
    public void ShouldBeAbleToFollowPathToTheGoal()
    {
        pathReference.UpdateAll();

        var startNode = grid[0, 0];
        var targetNode = grid[4, 4];
        FollowPath(pathReference, startNode, targetNode, 100);

        startNode = grid[4, 4];
        targetNode = grid[0, 0];
        FollowPath(pathReference, startNode, targetNode, 100);

        // test to ensure that reference returns same result as pathfinder's
        var pathFromReference = pathReference.FindPath(startNode, targetNode);
        var pathFromPathfinder = pathfinder.FindPath(startNode, targetNode);

        Assert.AreEqual(pathFromReference.Count, pathFromPathfinder.Count);

        for(int i = 0; i < pathFromReference.Count; i++)
        {
            Assert.AreEqual(pathFromReference[i], pathFromPathfinder[i]);
        }
    }

    void FollowPath(PathReference reference, Node source, Node target, int maxTry)
    {
        int step = 0;
        var currentNode = source;
        while (currentNode != target)
        {
            var nextNode = pathReference.GetNextNodeToTarget(currentNode, target);
            currentNode = nextNode;
            if (currentNode == target)
            {
                break;
            }

            step++;
            if (maxTry < step)
            {
                Assert.Fail("Could not follow path to the goal");
            }
        }
    }

    //[Test]
    public void ShouldPathfindToAllOfOtherWalkableNodes()
    {
        var pathfinderMock = Substitute.For<IPathfinder>();
        pathReference = new PathReference(grid, pathfinderMock);

        int x, y;
        x = 0;
        y = 0;

        var sourceNode = grid[x, y];
        pathReference.UpdatePathReference(x, y);
        foreach (Node targetNode in grid)
        {
            if (targetNode == sourceNode) continue;
            if (targetNode.walkable)
            {
                pathfinderMock.Received().FindPath(sourceNode, targetNode);
            }
            else
            {
                pathfinderMock.DidNotReceive().FindPath(sourceNode, targetNode);
            }
        }
    }

    //[Test]
    public void ShouldIndicateNextStep()
    {
        pathReference.UpdatePathReference(2, 2);

        var source = grid[2, 2];
        var target = grid[0, 0];

        var next = pathReference.GetNextNodeToTarget(source, target);
        Assert.AreEqual(grid[2, 3], next);

        target = grid[4, 4];
        next = pathReference.GetNextNodeToTarget(source, target);
        Assert.AreEqual(grid[2, 1], next);
    }
}
