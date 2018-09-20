using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] tileList;
    [SerializeField] GameObject[] wallTileList;
    [SerializeField] float tileSize;
    [SerializeField] GameObject spawnSpot;
    GameObject myPlayer;
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player");
        SpawnTile();
        SpawnTile();
        SpawnTile();
    }

    private void Update()
    {
        SetTileDifficulty();
        CheckDistance();
        //MoveLava();
    }

    float spawnTileDistance = 12;
    float lastSpawnSpot = 0;
    void CheckDistance()
    {
        if(myPlayer.transform.position.y > lastSpawnSpot + spawnTileDistance)
        {
            lastSpawnSpot = myPlayer.transform.position.y;
            SpawnTile();
        }
    }

    int tileToSpawn;
    int wallToSpawn;
    int lastWall = 0;
    int lastTile = 0;
    int SetRandomNumber(int min, int max,int lastNum)//set a random number ensuring that it is not the same as the last
    {
        int numToReturn = Random.Range(min,max);
        if (numToReturn == lastNum)
        {
            return SetRandomNumber(min, max, lastNum);
        }
        else return numToReturn;
    }

    [SerializeField] int diffucultyIncrementDistance;
    float lastIncrease = 0;
    int currentDifficulty = 0;
    void SetTileDifficulty()
    {
        if (myPlayer.transform.position.y > lastIncrease + diffucultyIncrementDistance)
        {
            currentDifficulty++;
            lastIncrease += diffucultyIncrementDistance;
        }
    }

    void SpawnTile()
    {
        switch (currentDifficulty)
        {
            case 0:
                wallToSpawn = 0;
                tileToSpawn = SetRandomNumber(1, 10, lastTile);
                break;
            case 1:
                wallToSpawn = SetRandomNumber(1,10,lastWall);
                tileToSpawn = SetRandomNumber(11, 21, lastTile);
                break;
            case 2:
                wallToSpawn = SetRandomNumber(11, 21, lastWall);
                tileToSpawn = SetRandomNumber(22, 32, lastTile);
                break;
            default:
                wallToSpawn = 0;
                tileToSpawn = SetRandomNumber(1, 10, lastTile);
                break;
        }
        lastWall = wallToSpawn;
        lastTile = tileToSpawn;
        Instantiate(tileList[tileToSpawn], spawnSpot.transform.position,tileList[tileToSpawn].transform.rotation);
        Instantiate(wallTileList[wallToSpawn], spawnSpot.transform.position, wallTileList[wallToSpawn].transform.rotation);
        transform.Translate(new Vector3(0, tileSize, 0));
    }

    [SerializeField] GameObject deathLava;
    [SerializeField] GameObject deathLavaAncorTop;//attached to the camera and placed at the top of the view so the lava follows
    [SerializeField] GameObject deathLavaAncorBot;
    [SerializeField] float damping;
    float lavaSpeed;
    void MoveLava()
    {
        deathLava.transform.position = new Vector3(0, Mathf.Lerp(deathLava.transform.position.y, deathLavaAncorTop.transform.position.y, Time.fixedDeltaTime * damping), 0);
        if (deathLava.transform.position.y <= deathLavaAncorBot.transform.position.y)
        {
            deathLava.transform.position = new Vector3(0, deathLavaAncorBot.transform.position.y, 0);
        }
    }
}
