using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRoom : MonoBehaviour
{
    [SerializeField] protected GameObject wallPrefab;
    protected GameObject[] dungeonWalls;
    protected int scale = 10;
    protected int m_sizeX;
    protected int m_sizeZ;
    protected int maxX = 4;
    protected int maxZ = 4;
    protected int minX = 2;
    protected int minZ = 2;
    // ENCAPSULATION: Ensure that room size is always > 0
    public int sizeX{
        get
        {
            return m_sizeX;
        }
        set
        {
            if (value > 0)
            {
                m_sizeX = value;
            }
            else
            {
                Debug.LogError("Attempting to set room size to negetive value");
            }
        }
    }
    public int sizeZ{
        get
        {
            return m_sizeZ;
        }
        set
        {
            if (value > 0)
            {
                m_sizeZ = value;
            }
            else
            {
                Debug.LogError("Attempting to set room size to negetive value");
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        setRoomSize(5,5);
        dungeonWalls = new GameObject[4];
        GameObject wall = (GameObject)Instantiate(wallPrefab);
        DungeonWall Dwall = wall.GetComponent<DungeonWall>();
        Dwall.hasDoor = true;
        Dwall.sizeX = sizeX * scale;
        Dwall.sizeY = 5;
        Dwall.sizeZ = 1;
        Dwall.addDoor(40, transform.position);
        dungeonWalls[0] = wall;
        for (int i = 1; i < 4; i++)
        {
            dungeonWalls[i] = (GameObject)Instantiate(wallPrefab);
            dungeonWalls[i].GetComponent<DungeonWall>().sizeX = sizeX * scale;
            dungeonWalls[i].GetComponent<DungeonWall>().sizeY = 5;
            dungeonWalls[i].GetComponent<DungeonWall>().sizeZ = 1;
            dungeonWalls[i].GetComponent<DungeonWall>().noDoor(transform.position);
        }
        dungeonWalls[0].transform.Translate(25, 0, 0);
        dungeonWalls[1].transform.Translate(-25, 0, 0);
        dungeonWalls[2].transform.Translate(0, 0, 25);
        dungeonWalls[3].transform.Translate(0, 0, -25);
        dungeonWalls[0].transform.Rotate(Vector3.up, 90);
        dungeonWalls[1].transform.Rotate(Vector3.up, 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setRandomRoomSize()
    {
        sizeX = Random.Range(minX, maxX);
        sizeZ = Random.Range(minZ, maxZ);
        transform.localScale = new Vector3(sizeX, 1, sizeZ);
    }

    void setRoomSize(int x, int z)
    {
        sizeX = x;
        sizeZ = z;
        transform.localScale = new Vector3(sizeX, 1, sizeZ);
    }
}
