using UnityEngine;
using System.Collections;
using UnityEditor;
using Pathfinding;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{

    public override void OnInspectorGUI()
    {

        MapGenerator mapGenerator = target as MapGenerator;

        if (DrawDefaultInspector())
        {
            mapGenerator.GenerateMap();
        }

        if (GUILayout.Button("Generate Map"))
        {
            mapGenerator.GenerateMap();
        }
        IMap currentMap = mapGenerator.CurrentMap;
        AstarPath astarPath = FindObjectOfType<AstarPath>();
    }

}

[CustomEditor(typeof(MapCreationStrategy))]
public class MapCreationStrategyEditor : Editor
{

    public override void OnInspectorGUI()
    {

        MapCreationStrategy strategy = target as MapCreationStrategy;

        if (DrawDefaultInspector())
        {
            strategy.GenerateMap();
        }
    }
}