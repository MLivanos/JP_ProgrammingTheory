using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    private List<(int,int)> frontier;
    private List<(int,int)> dungeon;
    [SerializeField] private GameObject roomPrefab;
    private int roomsInLevel = 16;
    private int nRooms = 1;
    private float xLength = 9.5f;
    private float zLength = 9.75f;

    void Start()
    {
        frontier = new List<(int,int)>();
        dungeon = new List<(int,int)>();
        GenerateRoom(0,0);
        GenerateLevel();
    }

    void GenerateLevel()
    {
        while(nRooms < roomsInLevel)
        {
            int index = Random.Range(0,frontier.Count);
            GenerateRoom(frontier[index].Item1, frontier[index].Item2);
            frontier.RemoveAt(index);
        }
    }

    void GenerateRoom(float x, float z)
    {
        int i_x = (int)x;
        int i_z = (int)z;
        GameObject roomGO = Instantiate(roomPrefab, new Vector3(x * xLength, 0, z * zLength), roomPrefab.transform.rotation);
        DungeonRoomController room = roomGO.GetComponent<DungeonRoomController>();
        room.SetUp();
        dungeon.Add((i_x, i_z));
        AddToFrontier(i_x, i_z);
        nRooms++;
    }

    void AddToFrontier(int x, int z)
    {
        bool[] canAddInQuadrant = new bool[4];
        canAddInQuadrant[0] = true;
        canAddInQuadrant[1] = true;
        canAddInQuadrant[2] = true;
        canAddInQuadrant[3] = true;
        (int,int) toNorth = (x,z+1);
        (int,int) toEast = (x+1,z);
        (int,int) toSouth = (x,z-1);
        (int,int) toWest = (x-1,z);
        for(int i=0; i<dungeon.Count; i++)
        {
            if(dungeon[i] == toNorth)
            {
                canAddInQuadrant[0] = false;
            }
            if(dungeon[i] == toEast)
            {
                canAddInQuadrant[1] = false;
            }
            if(dungeon[i] == toSouth)
            {
                canAddInQuadrant[2] = false;
            }
            if(dungeon[i] == toWest)
            {
                canAddInQuadrant[3] = false;
            }
        }
        if(canAddInQuadrant[0])
        {
            frontier.Add((toNorth));
        }
        if(canAddInQuadrant[1])
        {
            frontier.Add((toEast));
        }
        if(canAddInQuadrant[2])
        {
            frontier.Add((toSouth));
        }
        if(canAddInQuadrant[3])
        {
            frontier.Add((toWest));
        }
    }


}
