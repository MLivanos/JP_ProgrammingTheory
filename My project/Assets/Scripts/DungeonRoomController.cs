using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoomController : MonoBehaviour
{
    [SerializeField] GameObject eastDoor;
    [SerializeField] GameObject westDoor;
    [SerializeField] GameObject northDoor;
    [SerializeField] GameObject southDoor;
    [SerializeField] GameObject eastDoorBlock;
    [SerializeField] GameObject westDoorBlock;
    [SerializeField] GameObject northDoorBlock;
    [SerializeField] GameObject southDoorBlock;
    private GameObject[] doors;
    private GameObject[] doorBlockers;
    private bool[] opening;
    private float[] doorRotationOffsets;
    public GameObject[] neighbors{get; private set;}

    void Start()
    {

    }

    void Update()
    {
        for(int i = 0; i < opening.Length; i ++)
        {
            if(opening[i])
            {
                SwingDoor(i);
            }
        }
    }

    public void SetUp()
    {
        northDoor.SetActive(false);
        eastDoor.SetActive(false);
        southDoor.SetActive(false);
        westDoor.SetActive(false);
        doors = new GameObject[4];
        doorBlockers = new GameObject[4];
        doorRotationOffsets = new float[4];
        opening = new bool[4];
        doors[0] = northDoor;
        doors[1] = eastDoor;
        doors[2] = southDoor;
        doors[3] = westDoor;
        doorBlockers[0] = northDoorBlock;
        doorBlockers[1] = eastDoorBlock;
        doorBlockers[2] = southDoorBlock;
        doorBlockers[3] = westDoorBlock;
    }

    // Clockwise quadrant notation (0 is north, 1 east)
    void GiveDoor(int quadrant)
    {
        doorBlockers[quadrant].SetActive(false);
        doors[quadrant].SetActive(true);
    }

    void OpenDoor(int quadrant)
    {
        opening[quadrant] = true;
    }

    void SwingDoor(int quadrant)
    {
        Vector3 hinge;
        Vector3 offset;
        if (quadrant == 0 || quadrant == 2)
        {
            offset = new Vector3(1,0,0);
            hinge = offset + doors[quadrant].transform.position;
        }
        else
        {
            offset = new Vector3(0,0,1);
            hinge = offset + doors[quadrant].transform.position;
        }
        doors[quadrant].transform.RotateAround(hinge, Vector3.up, 50 * Time.deltaTime);
        doors[quadrant].transform.Translate(offset * 0.011f);
        doorRotationOffsets[quadrant] += 50 * Time.deltaTime;
        if (doorRotationOffsets[quadrant] >= 90)
        {
            opening[quadrant] = false;
        }
    }
}
