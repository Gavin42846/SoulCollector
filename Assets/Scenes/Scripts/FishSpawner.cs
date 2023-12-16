using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Fruit, Enemy, FastSoul, Heart;
    public GameObject canvas;
    public GameObject UICanvas;
    private float fishSpawnTimer = 0.0f;
    private float fishSpawnInterval = 1.5f;
    private float enemySpawnTimer = 0.0f;
    private float enemySpawnInterval = 15.0f;
    private float fishSpawnY, heartTimer, heartInterval;
    private float randomsoulSpawnTimer = 0.0f;
    private float randomsoulSpawnInterval = 6.0f;
    private float currentTimer, currentUpdateNum, lastUpdateNum;

    void Start()
    {
        FishSpawn(); 
        HeartRandTimer();  
    }

    void Update()
    {
        GetTimer();
        UpdateEnemySpawnRate();
        fishSpawnyBoi();
        EnemySpawnyBoi();
        RandomSoulSpawnTimer();
        HeartSpawnTimer();
    }
    void HeartRandTimer()
    {
        heartInterval = Random.Range(20f, 35f);
    }
    void HeartSpawnTimer()
    {
        heartTimer += Time.deltaTime;
        if(heartTimer >= heartInterval)
        {
            heartTimer -= heartInterval;
            HeartSpawn();           
        }
    }

    void GetTimer()
    {
        currentTimer = FindObjectOfType<Combo>().ReturnTimer();
    }
    void UpdateEnemySpawnRate()
    {
        //round down to see how many min it has been by dividing timer by 60
        float minutesNum = currentTimer/35f;
        int roundedMinutes = Mathf.FloorToInt(minutesNum);
        currentUpdateNum = roundedMinutes;
        //modify stats bases on how many min it has been
        if(roundedMinutes == 0 || (currentUpdateNum == lastUpdateNum) || enemySpawnInterval < 6)
        {
            return;
        }
        lastUpdateNum = currentUpdateNum;
        fishSpawnInterval -= fishSpawnInterval*0.1f;
        //this should be 5.8 sec at lowest interval
        enemySpawnInterval -= enemySpawnInterval*0.1f;
    }
    void fishSpawnyBoi()
    {
        fishSpawnTimer += Time.deltaTime;
        if(fishSpawnTimer >= fishSpawnInterval)
        {
            FishSpawn();
            fishSpawnTimer -= fishSpawnInterval;
        }

    }

    void EnemySpawnyBoi()
    {
        enemySpawnTimer += Time.deltaTime;
        if(enemySpawnTimer >= enemySpawnInterval)
        {
            EnemySpawn();
            enemySpawnTimer = 0f;
        }

    }

    void RandomSoulSpawnTimer()
    {
        randomsoulSpawnTimer += Time.deltaTime;
        if(randomsoulSpawnTimer >= randomsoulSpawnInterval)
        {
            RandomSoulSpawn();
            randomsoulSpawnTimer -= randomsoulSpawnInterval;
        }
        
    }

    private void RandomSoulSpawn()
    {
        int soulInt;
        soulInt = Random.Range(1, 100);

        if(soulInt > 0 && soulInt < 101)
        {
            var fastSoul = Instantiate(FastSoul, new Vector3(-10, 0, 0), Quaternion.identity);
            fastSoul.transform.parent = canvas.transform;
            fastSoul.transform.localScale = new Vector3(2f,1.75f,1f);
            fastSoul.transform.position = new Vector3(-10,fishSpawnY,0);

        }

    }

    private void FishSpawn()
    {
        //random number for Y coord for the fish to spawn within range of -2 and 2
        fishSpawnY = Random.Range(-2f, 2f);

        //spawns fish in random parameters set above then sets to child of canvas and moves fish to right location
        var testFish = Instantiate(Fruit, new Vector3(-10, 0, 0), Quaternion.identity);
        testFish.transform.parent = canvas.transform;
        testFish.transform.localScale = new Vector3(3f,2.5f,1f);
        testFish.transform.position = new Vector3(-10,fishSpawnY,0);
    }

    private void HeartSpawn()
    {
        //random number for Y coord for the fish to spawn within range of -2 and 2
        fishSpawnY = Random.Range(-2f, 2f);

        //spawns fish in random parameters set above then sets to child of canvas and moves fish to right location
        var heart = Instantiate(Heart, new Vector3(-10, 0, 0), Quaternion.identity);
        heart.transform.parent = canvas.transform;
        heart.transform.localScale = new Vector3(1.5f,1.5f,1f);
        heart.transform.position = new Vector3(-10,fishSpawnY,0);
        HeartRandTimer();

    }

    private void EnemySpawn()
    {
        float enemyspawnY = Random.Range(-2f, 2f);
        float enemyspawnX = Random.Range(-5f, 5f);
        var EnemyClone = Instantiate(Enemy, new Vector3(0, 0, 0), Quaternion.identity);
        EnemyClone.transform.parent = UICanvas.transform;
        EnemyClone.transform.localScale = new Vector3(0.15f,0.15f,1f);
        EnemyClone.transform.position = new Vector3(enemyspawnX,enemyspawnY,0);       
    }


}
