using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject[] enemyTypes;
    private int minEnemies = 1;
    private int maxEnemies = 3;
    public int roomX = 0;
    public int roomZ = 0;
    private float scaleX = 9.75f;
    private float scaleZ = 9.5f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GenerateEnemies(Random.Range(minEnemies, maxEnemies));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateEnemies(int nEnemies)
    {
        for(int i = 0; i < nEnemies; i++)
        {
            GameObject enemyChoice = enemyTypes[Random.Range(0, enemyTypes.Length)];
            float xPlacement = Random.Range(-4, 4) + roomX * scaleX;
            float zPlacement = Random.Range(-4, 4) + roomZ * scaleZ;;
            GameObject enemyGO = (GameObject)Instantiate(enemyChoice, new Vector3(xPlacement, enemyChoice.transform.position.y, zPlacement), enemyChoice.transform.rotation);
            Enemy enemy = enemyGO.GetComponent<Enemy>();
            enemy.SetTarget(player);
        }
    }
}
