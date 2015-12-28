using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PathReference : IPathReference
{
    IGrid grid;
    IPathfinder pathfinder;
    PathReferenceContainer[,] reference;
    public PathReference(IGrid _grid, IPathfinder _pathfinder)
    {
        grid = _grid;
        pathfinder = _pathfinder;
        reference = new PathReferenceContainer[grid.Width, grid.Height];
    }

    public Node GetNextNodeToTarget(Node source, Node target)
    {
        int x, y;
        x = source.gridX;
        y = source.gridY;

        return reference[x, y].GetNextNodeToTarget(target);
    }

    public void UpdateAll()
    {
        for (int y = 0; y < grid.Height; y++)
        {
            for (int x = 0; x < grid.Width; x++)
            {
                UpdatePathReference(x, y);
            }
        }
    }

    public void UpdatePathReference(int x, int y)
    {
        if(reference[x, y] == null)
        {
            CreatePathReference(x, y);
        }

        reference[x, y].Update();
    }

    public void CreatePathReference(int x, int y)
    {
        reference[x, y] = new PathReferenceContainer(grid[x, y], grid, pathfinder);
    }

    private class PathReferenceContainer
    {
        Node source;
        IGrid grid;
        IPathfinder pathfinder;
        Dictionary<Node, Node> reference;
        public PathReferenceContainer(Node _source, IGrid _grid, IPathfinder _pathfinder)
        {
            source = _source;
            grid = _grid;
            pathfinder = _pathfinder;
            reference = new Dictionary<Node, Node>();
        }

        public void Update()
        {
            for(int y = 0; y < grid.Height; y++)
            {
                for(int x = 0; x < grid.Width; x++)
                {
                    var target = grid[x, y];
                    if (!target.walkable || target == source) continue;
                    var path = pathfinder.FindPath(source, target);
                    if (path == null || path.Count <= 0) continue;

                    var nextNodeToTarget = path[path.Count - 1];

                    reference[target] = nextNodeToTarget;
                }
            }
        }

        public Node GetNextNodeToTarget(Node target)
        {
            return reference[target];
        }
    }
}
