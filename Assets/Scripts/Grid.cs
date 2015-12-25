using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour
{
    public MapGenerator mapGenerator;

    public LayerMask unwalkableMask;
    public float nodeRadius;
    Node[,] grid;

    void OnDrawGizmos()
    {
        if (mapGenerator == null) return;

        Gizmos.DrawWireCube(transform.position, new Vector3(mapGenerator.mapSize.x, mapGenerator.mapSize.y, 1));
    }
}
