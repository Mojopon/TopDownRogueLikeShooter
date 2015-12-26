using UnityEngine;
using System.Collections.Generic;

public interface IGrid
{
    List<Node> GetNeightbours(Node node);
}
