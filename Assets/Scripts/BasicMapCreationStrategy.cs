using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BasicMapCreationStrategy
{
    protected class Room
    {
        public int left;
        public int top;
        public int width;
        public int height;

        public bool isConnected = false;

        public int right
        {
            get { return left + width - 1; }
        }

        public int bottom
        {
            get { return top + height - 1; }
        }

        public int center_x
        {
            get { return left + width / 2; }
        }

        public int center_y
        {
            get { return top + height / 2; }
        }

        public bool CollidesWith(Room other)
        {
            if (left > other.right - 1)
                return false;

            if (top > other.bottom - 1)
                return false;

            if (right < other.left + 1)
                return false;

            if (bottom < other.top + 1)
                return false;

            return true;
        }
    }

    private int[,] mapData;

    private Vector2 mapSize;
    private Vector2 maxRoomSize;
    private Vector2 minRoomSize;

    private List<Room> rooms;

    public BasicMapCreationStrategy() { }

    public int[,] Create(Vector2 mapSize, Vector2 maxRoomSize, Vector2 minRoomSize, int seed)
    {
        int mapSizeX = (int)mapSize.x;
        int mapSizeY = (int)mapSize.y;

        int maxRoomSizeX = (int)maxRoomSize.x;
        int maxRoomSizeY = (int)maxRoomSize.y;

        int minRoomSizeX = (int)maxRoomSize.x;
        int minRoomSizeY = (int)maxRoomSize.y;


        mapData = new int[mapSizeX, mapSizeY];
        rooms = new List<Room>();

        while(rooms.Count < 10)
        {

        }

        return mapData;
    }
}
