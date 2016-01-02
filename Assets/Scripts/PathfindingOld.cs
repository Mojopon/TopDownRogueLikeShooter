using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Diagnostics;

public class PathfindingOld : MonoBehaviour
{
    public List<Node> Path = new List<Node>();

    PathReference pathReference;
    PathRequestManager requestManager;
    Grid grid;
    IPathfinder pathfinder;

    public void Initialize()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<Grid>();
        pathfinder = new PathfindingImpl(grid);
    }

    public void StartFindPath(Vector3 startPos, Vector3 target)
    {
        StartCoroutine(FindPath(startPos, target));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        var path = pathfinder.FindPath(startNode, targetNode, out pathSuccess);

        yield return null;

        if (pathSuccess)
        {
            waypoints = SimplyfyPath(path);
            Array.Reverse(waypoints);
        }

        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] SimplyfyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i< path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }

        return waypoints.ToArray();
    }
}
