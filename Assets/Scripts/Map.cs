using UnityEngine;
using System.Collections;

[System.Serializable]
public class Map : IMap
{
    public int[,] MapData { get; set; }
}
