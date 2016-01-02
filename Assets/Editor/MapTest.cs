using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System;

[TestFixture]
public class MapTest
{
    [Test]
    public void ShouldReturnWidthAndHeight()
    {
        var tileData = new TileType[,]
        {
            { TileType.Floor, TileType.Floor, TileType.Floor, TileType.Floor },
            { TileType.Floor, TileType.Floor, TileType.Floor, TileType.Floor },
            { TileType.Floor, TileType.Floor, TileType.Floor, TileType.Floor },
        };

        var map = new Map();
        map.TileData = tileData;

        Assert.AreEqual(3, map.Width);
        Assert.AreEqual(4, map.Height);
    }
}
