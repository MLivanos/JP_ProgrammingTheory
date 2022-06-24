using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonWall : MonoBehaviour
{
    [SerializeField] protected GameObject subwallPrefab;
    [SerializeField] protected GameObject doorPrefab;
    protected int m_sizeX;
    protected int m_sizeY;
    protected int m_sizeZ;
    public bool hasDoor;
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
    public int sizeY{
        get
        {
            return m_sizeY;
        }
        set
        {
            if (value > 0)
            {
                m_sizeY = value;
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
    public float f_sizeX{
        get
        {
            return (float)m_sizeX;
        }
    }
    public float f_sizeY{
        get
        {
            return (float)m_sizeY;
        }
    }
    public float f_sizeZ{
        get
        {
            return (float)m_sizeZ;
        }
    }

    public void addDoor(int x, Vector3 pos)
    {
        Vector3 doorScale = doorPrefab.transform.localScale;
        float width;
        float height;
        float xOffset;
        float yOffset;
        // Make float copies of measures for math reasons
        float f_x = (float)x;
        // TODO: Make sure door can fit
        // Make wall to the -X of the door
        width = f_x;
        height = doorScale.y;
        xOffset = f_sizeX / 2 - f_x / 2;
        yOffset = doorScale.y / 2;
        GameObject westSubwall = (GameObject)Instantiate(subwallPrefab, pos, subwallPrefab.transform.rotation);
        westSubwall.transform.localScale = new Vector3(width, height, sizeZ);
        westSubwall.transform.Translate(new Vector3(xOffset, yOffset, 0));
        // Make wall to the X of the door
        width = sizeX - f_x - doorPrefab.transform.localScale.x;
        height = doorScale.y;
        float f_width = (float)width;
        xOffset = -1 * (f_sizeX - f_width) / 2;
        yOffset = doorScale.y / 2;
        GameObject eastSubwall = (GameObject)Instantiate(subwallPrefab, pos, subwallPrefab.transform.rotation);
        eastSubwall.transform.localScale = new Vector3(width, height, sizeZ);
        eastSubwall.transform.Translate(new Vector3(xOffset, yOffset, 0));
        // Make wall to the Y of the door
        width = f_sizeX;
        height = f_sizeY - doorScale.y;
        xOffset = 0;
        yOffset = height / 2 + doorScale.y;
        GameObject northSubwall = (GameObject)Instantiate(subwallPrefab, pos, subwallPrefab.transform.rotation);
        northSubwall.transform.localScale = new Vector3(width, height, sizeZ);
        northSubwall.transform.Translate(new Vector3(xOffset, yOffset, 0));
        // Make door
        xOffset = f_sizeX / 2 - f_x - doorScale.x / 2;
        yOffset = doorScale.y / 2;
        GameObject door = (GameObject)Instantiate(doorPrefab, pos, doorPrefab.transform.rotation);
        door.transform.Translate(new Vector3(xOffset, yOffset, 0));
        //Make subwalls children of parent
        westSubwall.transform.parent = gameObject.transform;
        eastSubwall.transform.parent = gameObject.transform;
        northSubwall.transform.parent = gameObject.transform;
        door.transform.parent = gameObject.transform;
    }

    public void noDoor(Vector3 pos)
    {
        GameObject subWall = (GameObject)Instantiate(subwallPrefab, pos, subwallPrefab.transform.rotation);
        subWall.transform.localScale = new Vector3(sizeX, sizeY, sizeZ);
        subWall.transform.Translate(0, f_sizeY / 2, 0);
        subWall.transform.parent = gameObject.transform;
    }
}
