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

    public IMap Create(Vector2 mapSize, Vector2 maxRoomSize, Vector2 minRoomSize, int seed)
    {
        int mapSizeX = (int)mapSize.x;
        int mapSizeY = (int)mapSize.y;

        int maxRoomSizeX = (int)maxRoomSize.x;
        int maxRoomSizeY = (int)maxRoomSize.y;

        int minRoomSizeX = (int)minRoomSize.x;
        int minRoomSizeY = (int)minRoomSize.y;


        mapData = new int[mapSizeX, mapSizeY];
        rooms = new List<Room>();

        int maxFails = 10;

        while(rooms.Count < 10)
        {
            int roomSizeX = Random.Range(minRoomSizeX, maxRoomSizeX);
            int roomSizeY = Random.Range(minRoomSizeY, maxRoomSizeY);

            var room = new Room();
            room.left = Random.Range(0, mapSizeX - roomSizeX);
            room.top = Random.Range(0, mapSizeY - roomSizeY);
            room.width = roomSizeX;
            room.height = roomSizeY;

            if(!RoomCollides(room))
            {
                rooms.Add(room);
            }
            else
            {
                maxFails--;
                if (maxFails <= 0)
                    break;
            }
        }

        foreach(Room room in rooms)
        {
            MakeRoom(room);
        }

        for (int i = 0; i < rooms.Count - 1; i++)
        {
            MakeCorridor(rooms[i], rooms[(i + 1)]);
        }

        MakeWalls();

        var map = new Map();
        map.MapData = mapData;
        return map;
    }

    bool RoomCollides(Room r)
    {
        foreach (Room r2 in rooms)
        {
            if (r.CollidesWith(r2))
            {
                return true;
            }
        }

        return false;
    }

    void MakeRoom(Room r)
    {
        for (int x = 0; x < r.width; x++)
        {
            for (int y = 0; y < r.height; y++)
            {
                mapData[r.left + x, r.top + y] = 1;
            }
        }
    }

    void MakeCorridor(Room r1, Room r2)
    {
        MakeCorridor(r1.center_x, r1.center_y, r2.center_x, r2.center_y);
    }

    void MakeCorridor(int sourceX, int sourceY, int targetX, int targetY)
    {
        int x = sourceX;
        int y = sourceY;

        while (x != targetX)
        {
            mapData[x, y] = 1;

            x += x < targetX ? 1 : -1;
        }

        while (y != targetY)
        {
            mapData[x, y] = 1;

            y += y < targetY ? 1 : -1;
        }
    }

    void MakeWalls()
    {
        for (int x = 0; x < mapData.GetLength(0); x++)
        {
            for (int y = 0; y < mapData.GetLength(1); y++)
            {
                if (mapData[x, y] == 0 && HasAdjacentFloor(x, y))
                {
                    mapData[x, y] = 2;
                }
            }
        }
    }

    bool HasAdjacentFloor(int x, int y)
    {
        if (x > 0 && mapData[x - 1, y] == 1)
            return true;
        if (x < mapData.GetLength(0) - 1 && mapData[x + 1, y] == 1)
            return true;
        if (y > 0 && mapData[x, y - 1] == 1)
            return true;
        if (y < mapData.GetLength(1) - 1 && mapData[x, y + 1] == 1)
            return true;

        if (x > 0 && y > 0 && mapData[x - 1, y - 1] == 1)
            return true;
        if (x < mapData.GetLength(0) - 1 && y > 0 && mapData[x + 1, y - 1] == 1)
            return true;

        if (x > 0 && y < mapData.GetLength(1) - 1 && mapData[x - 1, y + 1] == 1)
            return true;
        if (x < mapData.GetLength(0) - 1 && y < mapData.GetLength(1) - 1 && mapData[x + 1, y + 1] == 1)
            return true;


        return false;
    }
}
