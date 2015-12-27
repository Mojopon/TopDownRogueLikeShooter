using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(MapGenerator))]
public class MapEditor : Editor
{

    public override void OnInspectorGUI()
    {

        MapGenerator map = target as MapGenerator;

        if (DrawDefaultInspector())
        {
            map.GenerateMap();
        }

        if (GUILayout.Button("Generate Map"))
        {
            map.GenerateMap();
        }


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