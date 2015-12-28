using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IPathfinder
{
    List<Node> FindPath(Node startNode, Node targetNode);
    List<Node> FindPath(Node startNode, Node targetNode, out bool pathSuccess);
}
