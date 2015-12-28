using UnityEngine;
using System.Collections;

public interface IPathReference
{
    Node GetNextNodeToTarget(Node source, Node target);
}
