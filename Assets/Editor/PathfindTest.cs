using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class PathfindTest
{
    private Node[,] nodes;

    private int[,] map = new int[,]
    {
        { 0,1,0,0,0 },
        { 0,1,0,1,0 },
        { 0,1,0,1,0 },
        { 0,1,0,1,0 },
        { 0,0,0,1,0 },
    };

    [SetUp]
    public void InitializeNodes()
    {
        int mapSizeX = map.GetLength(0);
        int mapSizeY = map.GetLength(1);
        nodes = new Node[mapSizeX, mapSizeY];
        for (int y = 0; y < mapSizeY; y++)
        {
            for (int x = 0; x < mapSizeX; x++)
            {
                CreateNode(x, y, map[x, y] == 0);
            }
        }
    }

    [Test]
    public void ShouldFindPath()
    {

    }

    void CreateNode(int x, int y, bool walkable)
    {
        nodes[x, y] = new Node(walkable, Vector3.zero, x, y);
    }
}
