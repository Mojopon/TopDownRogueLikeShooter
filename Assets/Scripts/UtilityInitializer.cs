using UnityEngine;
using System.Collections;

public class UtilityInitializer : MonoBehaviour
{
    public MapGenerator mapGenerator;
    public Grid grid;
    public PathfindingOld pathfinding;
    public PathRequestManager pathRequestManager;

    void Awake()
    {
        mapGenerator.Initialize();
        grid.Initialize();
        pathfinding.Initialize();
        pathRequestManager.Initialize();
    }
}
