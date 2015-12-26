using UnityEngine;
using System.Collections;
using NUnit.Framework;
using NSubstitute;

[TestFixture]
public class GridTest
{
    GridImpl grid;
    [SetUp]
    public void SetUp()
    {
        grid = new GridImpl(10, 10);
    }

    [Test]
    public void CanSetandGetGrid()
    {
        Node node = new Node(true, Vector3.zero, 3, 3);
        grid[0, 0] = node;
        Assert.AreEqual(node, grid[0, 0]);
    }
}
