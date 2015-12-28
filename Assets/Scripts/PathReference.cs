using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class PathReference : IPathReference, IPathfinder
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
        for (int y = 0; y < reference.GetLength(1); y++)
        {
            for (int x = 0; x < reference.GetLength(0); x++)
            {
                UpdatePathReference(x, y);
            }
        }
    }

    public void UpdatePathReference(int x, int y)
    {
        if (!grid[x, y].walkable) return;

        Stopwatch sw = new Stopwatch();
        sw.Start();

        if (reference[x, y] == null)
        {
            CreatePathReference(x, y);
        }

        reference[x, y].Update();

        sw.Stop();
        UnityEngine.Debug.Log(sw.ElapsedMilliseconds);
    }

    public void CreatePathReference(int x, int y)
    {
        reference[x, y] = new PathReferenceContainer(grid[x, y], grid, pathfinder);
    }

    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        bool pathSuccess;
        return FindPath(startNode, targetNode, out pathSuccess);
    }

    public List<Node> FindPath(Node startNode, Node targetNode, out bool pathSuccess)
    {
        var path = new List<Node>();
        path.Add(targetNode);

        var currentNode = targetNode;
        while(currentNode != startNode)
        {
            var next = GetNextNodeToTarget(currentNode, startNode);
            if (next == null || next == startNode) break;

            path.Add(next);
            currentNode = next;
        }

        if (path.Count <= 1)
        {
            pathSuccess = false;
            path.Clear();
        }else
        {
            pathSuccess = true;
        }

        return path;
    }

    private class PathReferenceContainer
    {
        Node source;
        IGrid grid;
        IPathfinder pathfinder;
        Coord?[,] reference;
        public PathReferenceContainer(Node _source, IGrid _grid, IPathfinder _pathfinder)
        {
            source = _source;
            grid = _grid;
            pathfinder = _pathfinder;
            reference = new Coord?[grid.Width, grid.Height];
        }

        public void Update()
        {
            if (!source.walkable) return;

            for(int y = 0; y < grid.Height; y++)
            {
                for(int x = 0; x < grid.Width; x++)
                {
                    var target = grid[x, y];
                    if (!target.walkable || target == source) continue;
                    var path = pathfinder.FindPath(source, target);
                    if (path == null || path.Count <= 0) continue;

                    var nextNodeToTarget = path[path.Count - 1];

                    reference[target.gridX, target.gridY] = new Coord(nextNodeToTarget.gridX, nextNodeToTarget.gridY);
                }
            }
        }

        public Node GetNextNodeToTarget(Node target)
        {
            if (reference[target.gridX, target.gridY] == null) return null;

            var nextStepCoord = reference[target.gridX, target.gridY];
            return grid[nextStepCoord.Value.x, nextStepCoord.Value.y];
        }
    }
}
