using UnityEngine;
using System.Collections;
using System;

public class MapCreationStrategy : MonoBehaviour
{
    public MapCreationStrategyType strategy;
    public Vector2 mapSize;
    public int seed;

    public SettingsForBasicMapCreation settingsForBasicMapCreation;

    public void GenerateMap()
    {
        var mapGenerator = GetComponent<MapGenerator>();
        if (mapGenerator != null) mapGenerator.GenerateMap();
    }

    public Map Create()
    {
        Map map = CreateBasicMap();
        map.MapSize = mapSize;
        return map;
    }

    private Map CreateBasicMap()
    {
        var mapCreationStrategy = new BasicMapCreationStrategyImpl();
        var maxRoomSize = settingsForBasicMapCreation.maxRoomSize;
        var minRoomSize = settingsForBasicMapCreation.minRoomSize;

        Debug.Log("max room size : " + maxRoomSize);
        Debug.Log("min room size : " + minRoomSize);

        return mapCreationStrategy.Create(mapSize, maxRoomSize, minRoomSize, seed);
    }

    [Serializable]
    public class SettingsForBasicMapCreation
    {
        public Vector2 maxRoomSize;
        public Vector2 minRoomSize;
    }
}
