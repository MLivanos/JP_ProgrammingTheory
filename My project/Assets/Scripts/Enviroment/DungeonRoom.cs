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
        setRandomRoomSize();
        dungeonWalls = new GameObject[4];
        dungeonWalls[0] = addDooredWall(sizeX, 5, Random.Range(5,scale * sizeX - 5));
        dungeonWalls[1] = addWall(sizeX, 5);
        dungeonWalls[2] = addWall(sizeZ, 5);
        dungeonWalls[3] = addWall(sizeZ, 5);
        placeWalls(dungeonWalls);
        assignChildren(dungeonWalls);
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

    GameObject addDooredWall(int wallLength, int wallHeight, int doorPos)
    {
        GameObject wall = (GameObject)Instantiate(wallPrefab);
        DungeonWall Dwall = wall.GetComponent<DungeonWall>();
        Dwall.hasDoor = true;
        Dwall.sizeX = wallLength * scale;
        Dwall.sizeY = wallHeight;
        Dwall.sizeZ = 1;
        Dwall.addDoor(doorPos, transform.position);
        return wall;
    }

    GameObject addWall(int wallLength, int wallHeight)
    {
        GameObject wall = (GameObject)Instantiate(wallPrefab);
        DungeonWall Dwall = wall.GetComponent<DungeonWall>();
        Dwall.sizeX = wallLength * scale;
        Dwall.sizeY = wallHeight;
        Dwall.sizeZ = 1;
        Dwall.noDoor(transform.position);
        return wall;
    }

    void placeWalls(GameObject[] walls)
    {
        walls[3].transform.Translate(sizeX * scale / 2, 0, 0);
        walls[2].transform.Translate(-1 * sizeX * scale / 2, 0, 0);
        walls[1].transform.Translate(0, 0, sizeZ * scale / 2);
        walls[0].transform.Translate(0, 0, -1 * sizeZ * scale / 2);
        walls[3].transform.Rotate(Vector3.up, 90);
        walls[2].transform.Rotate(Vector3.up, 90);
    }

    void assignChildren(GameObject[] walls)
    {
        for(int i = 0; i < walls.Length; i++)
        {
            walls[i].transform.parent = gameObject.transform;
        }
    }
}
