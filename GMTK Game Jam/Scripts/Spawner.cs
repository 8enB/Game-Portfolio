using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;

    public GameObject[] enemysWave1;
    public GameObject[] enemysWave2;
    public GameObject[] enemysWave3;
    public GameObject[] enemysWave4;
    public GameObject[] enemysWave5;

    public int xMax;
    public int xMin;
    public int yMax;
    public int yMin;

    public float minDistFromPlayer;
    public float maxDistFromPlayer;

    public float timeBetweenSpawns;
    private float timer;

    public float[] timeForSpawn;

    public bool shouldSpawn = true;

    public int allowedAmmount;
    public int currentAmmount = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmmount > allowedAmmount)
        {
            timeBetweenSpawns = 10;
        }
        int spawnPointX = Random.Range(xMin, xMax);
        int spawnPointY = Random.Range(yMin, yMax);
        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, 0);

        timer += Time.deltaTime;
        if (timer > timeBetweenSpawns)
        {
            shouldSpawn = true;
        }

        if(PlayerControl.instance.bombTime > 0)
        {
            shouldSpawn = false;
        }
        
        if (shouldSpawn && Vector3.Distance(PlayerControl.instance.transform.position, spawnPosition) < maxDistFromPlayer && Vector3.Distance(PlayerControl.instance.transform.position, spawnPosition) > minDistFromPlayer)
        {
            if (Manager.instance.currentWave == 1)
            {
                timeBetweenSpawns = timeForSpawn[0];
                timer = 0;
                Instantiate(enemysWave1[UnityEngine.Random.Range(0, enemysWave1.Length - 1)], spawnPosition, transform.rotation);
                //currentAmmount++;
                shouldSpawn = false;
            }
            if (Manager.instance.currentWave == 2)
            {
                timeBetweenSpawns = timeForSpawn[1];
                timer = 0;
                Instantiate(enemysWave2[UnityEngine.Random.Range(0, enemysWave2.Length - 1)], spawnPosition, transform.rotation);
                //currentAmmount++;
                shouldSpawn = false;
            }
            if (Manager.instance.currentWave == 3)
            {
                timeBetweenSpawns = timeForSpawn[2];
                timer = 0;
                Instantiate(enemysWave3[UnityEngine.Random.Range(0, enemysWave3.Length - 1)], spawnPosition, transform.rotation);
                //currentAmmount++;
                shouldSpawn = false;
            }
            if (Manager.instance.currentWave == 4)
            {
                timeBetweenSpawns = timeForSpawn[3];
                timer = 0;
                Instantiate(enemysWave4[UnityEngine.Random.Range(0, enemysWave4.Length - 1)], spawnPosition, transform.rotation);
                //currentAmmount++;
                shouldSpawn = false;
            }
            if (Manager.instance.currentWave == 5)
            {
                timeBetweenSpawns = timeForSpawn[4];
                timer = 0;
                Instantiate(enemysWave5[UnityEngine.Random.Range(0, enemysWave5.Length - 1)], spawnPosition, transform.rotation);
                //currentAmmount++;
                shouldSpawn = false;
            }
        }
    }
}
