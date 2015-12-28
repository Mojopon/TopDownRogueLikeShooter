using UnityEngine;
using System.Collections.Generic;

public interface IGrid
{
    Node this[int x, int y] { get; }
    List<Node> GetNeightbours(Node node);
    int MaxSize { get; }
    int Width { get; }
    int Height { get; }
}
