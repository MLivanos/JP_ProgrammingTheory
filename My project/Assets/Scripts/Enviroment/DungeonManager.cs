using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private GameObject dungeonRoomPrefab;
    private List<string> frontier;
    private DungeonRoom[] rooms;
    private int[,] positions;
    private int[,] envrioment;
    private int maxX = 20;
    private int maxZ = 20;
    private int roomsInLevel = 3;
    private int counter = 1;
    private int scale = 10;
    private bool hasMovedRooms = false;

    // Start is called before the first frame update
    void Start()
    {
        envrioment = new int[maxZ, maxX];
        frontier = new List<string>();
        rooms = new DungeonRoom[roomsInLevel];
        positions = new int[roomsInLevel,2];
        AddRoom(0,0);
        AddRoom(1,1);
        AddRoom(5,5);
    }

    void Update()
    {
        if (!hasMovedRooms && counter > roomsInLevel)
        {
            MoveRooms();
            hasMovedRooms = true;
        }
    }

    // TODO: Check if placement is ok
    // TODO: Check if overlap
    bool AddRoom(int x, int z)
    {
        int scaledX = x * scale;
        int scaledZ = z * scale;
        GameObject dungeonRoomGO = Instantiate(dungeonRoomPrefab, new Vector3(0,0,0), dungeonRoomPrefab.transform.rotation);
        DungeonRoom room = dungeonRoomGO.GetComponent<DungeonRoom>();
        room.SetUp();
        rooms[counter - 1] = room;
        if (!CanAddAtLocation(x, z, room.sizeX, room.sizeZ))
        {
            Destroy(dungeonRoomGO);
            AddRoom(10,10);
            return false;
        }
        positions[counter - 1, 0] = scaledX + room.sizeX*scale;
        positions[counter - 1, 1] = scaledZ + room.sizeZ*scale;
        int[,] frontierCandidates = new int[8 * (room.sizeZ+room.sizeX),2];
        int candidateCounter = 0;
        for(int i = z; i < room.sizeZ + z; i++)
        {
            for(int j = x; j < room.sizeX + x; j++)
            {
                envrioment[i,j] = counter;
                frontierCandidates[candidateCounter, 0] = j;
                frontierCandidates[candidateCounter, 1] = i;
                candidateCounter ++;
            }
        }
        for(int i=0; i<candidateCounter; i++)
        {
            AddToFrontier(frontierCandidates[i, 0], frontierCandidates[i, 1]);
        }
        counter++;
        return true;
    }

    bool CanAddAtLocation(int x, int z, int xSize, int zSize)
    {
        for(int i = z; i < z + zSize; i++)
        {
            for(int j = x; j < x + xSize; j++)
            {
                if (envrioment[i, j] != 0)
                {
                    return false;
                }
            }
        }
        return true;
    }

    void MoveRooms()
    {
        for(int i=0; i<roomsInLevel;i++)
        {
            rooms[i].transform.Translate(positions[i,0],0,positions[i,1]);
        }
    }

    void AddToFrontier(int x, int z)
    {
        bool xPlus = x < maxX;
        bool xMinus = x > 0;
        bool zPlus = z < maxZ;
        bool zMinus = z > 0;

        if (xPlus && zPlus && envrioment[z+1, x+1] == 0)
        {
            frontier.Add((x+1) + "," + (z+1));
        }
        if (zPlus && envrioment[z+1, x] == 0)
        {
            frontier.Add((x) + "," + (z+1));
        }
        if (xMinus && zPlus && envrioment[z+1, x-1] == 0)
        {
            frontier.Add((x-1) + "," + (z+1));
        }
        if (xPlus && envrioment[z, x+1] == 0)
        {
            frontier.Add((x+1) + "," + (z));
        }
        if (xMinus && envrioment[z+1, x-1] == 0)
        {
            frontier.Add((x-1) + "," + (z));
        }
        if (xMinus && zMinus && envrioment[z-1, x-1] == 0)
        {
            frontier.Add((x-1) + "," + (z-1));
        }
        if (zMinus && envrioment[z-1, x] == 0)
        {
            frontier.Add((x) + "," + (z-1));
        }
        if (xPlus && zMinus && envrioment[z-1, x+1] == 0)
        {
            frontier.Add((x+1) + "," + (z-1));
        }
    }
}
