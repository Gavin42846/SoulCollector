using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyController : MonoBehaviour
{
    GameObject[] Fruit;
    private Rigidbody2D body;
    private float moveSpeed = 2.0f;
    private float eatRange = 0.5f;
    private bool inRange;
    public Vector2 velocity = new Vector2(1f, 1f);
    private float lastfishDistance = -1f;
    Vector2 fishPos, enemyPos, oldfishPos, oldenemyPos;
    private float oldX, oldY;
    private int currentSize = 1; 
    private int totalSize = 6;
    private bool eatCooldown = false;
    private float cooldownTimer = 5.0f;
    private float numTimer = 0f;
    private float currentTimer, currentUpdateNum, lastUpdateNum;
    

    // Start is called before the first frame update
    void Start()
    {
        GetTimer();
        UpdateEnemyAI();
        body = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame

    void Update()
    {
        GetTimer();
        UpdateEnemyAI();
        CoolDownCalc();
        EnemyUpdater();
    }
    private void OnDestroy()
    {
        
        
        
    }
    void CoolDownCalc()
    {
        if(eatCooldown)
        {
            numTimer += Time.deltaTime;
            if(numTimer >= cooldownTimer)
            {
                eatCooldown = false;
                numTimer = 0f;
            }
        }
    }
    void GetTimer()
    {
        currentTimer = FindObjectOfType<Combo>().ReturnTimer();
    }

    void UpdateEnemyAI()
    {
        //round down to see how many min it has been by dividing timer by 60
        float minutesNum = currentTimer/120f;
        int roundedMinutes = Mathf.FloorToInt(minutesNum);
        currentUpdateNum = roundedMinutes;
        //modify stats bases on how many min it has been
        if(roundedMinutes == 0 || (currentUpdateNum == lastUpdateNum) || totalSize < 4)
        {
            return;
        }
        //should only speedup when you hit a new min
        //Debug.Log("tried to update speed of normal fish");
        lastUpdateNum = currentUpdateNum;
        totalSize --;
        //put thing here to change color of enemy when this happens

        

    }

    void EnemyUpdater()
    {
        
        //sets array to how many fruits are on the map

        Fruit = GameObject.FindGameObjectsWithTag("Fruit");
        //Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if(GameObject.FindGameObjectsWithTag("Fruit").Length > 0)
        {           
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Fruit").Length; i++)
            {
                if((Vector3.Distance(body.transform.position,Fruit[i].transform.position) < eatRange)) //checks to see if fish is close to catch bar
                {
                    inRange = true;
                    break;
                }
                inRange = false;
                fishPos = new Vector2(Fruit[i].transform.position.x, Fruit[i].transform.position.y);
                enemyPos = new Vector2(body.transform.position.x, body.transform.position.y);
                float deltaX = fishPos.x - enemyPos.x;
                float deltaY = fishPos.y - enemyPos.y;
                float magnitude = Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY);
                if(magnitude < lastfishDistance || lastfishDistance == -1f)
                {
                    //Debug.Log("A");
                    lastfishDistance = magnitude;
                    oldenemyPos = enemyPos;
                    oldfishPos = fishPos;
                    //call function that moves fish using the current XY coords
                    DirectionSelector();
                }
                else 
                {
                    //Debug.Log("B");
                    fishPos = oldfishPos;
                    enemyPos = oldenemyPos;
                    DirectionSelector();
                }           
            }
            lastfishDistance = -1f;
            if(inRange && !eatCooldown)
            {
                //call eat fishy
                PickUpFish();
                FindObjectOfType<CatchBar>().EatSoulSound();
            }
        }
    }

    void DirectionSelector()
    {
        
        Vector2 direction = (fishPos - enemyPos).normalized;
        body.velocity = direction * moveSpeed;
        body.transform.Translate(velocity * Time.deltaTime);

    }


    public void PickUpFish()
    {
        GameObject closestFish = null;
        float closestDistance = eatRange;
        float newDistance;
        foreach (GameObject fruit in Fruit)
        {
            newDistance = Vector3.Distance(body.transform.position, fruit.transform.position);
            if (newDistance < eatRange && newDistance < closestDistance)
            {
                closestDistance = newDistance;
                closestFish = fruit;
            }
        }
        if (closestFish != null)
        {
            //destroys the fruit you pick up
            Destroy(closestFish);
            //call function to tick up the counter for eaten fish
            currentSize++;
            moveSpeed = 2.0f/(currentSize*0.5f);
            eatCooldown = true;
            body.transform.localScale = (body.transform.localScale)*1.15f;
            //Debug.Log(currentSize);
            if(currentSize >= totalSize)
            {
                FindObjectOfType<Combo>().KillHeart();
                Destroy(gameObject);
            }
        }
    }


    //TAKE FOR STATIONARY ENEMY LEGIT JUST COPY PASTE THIS STUFF XD
    /*
    GameObject[] Fruit;
    private Rigidbody2D body;
    public float moveSpeed;
    private float eatRange = 0.5f;
    private float eatenFish = 0f;
    private bool inRange;
    public Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();       
    }

    // Update is called once per frame
    void Update()
    {
            EnemyUpdater();
    }
    void EnemyUpdater()
    {
        //sets array to how many fruits are on the map

        Fruit = GameObject.FindGameObjectsWithTag("Fruit");
        //Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if(GameObject.FindGameObjectsWithTag("Fruit").Length > 0)
        {           
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Fruit").Length; i++)
            {
            if((Vector3.Distance(body.transform.position,Fruit[i].transform.position) < eatRange)) //checks to see if fish is close to catch bar
            {
                inRange = true;
                break;
            }
            inRange = false;
            }
            if(inRange == true)
            {
                //call eat fishy
                PickUpFish();
                //body.position = new Vector2(body.position.x, -4f);
            }
        }
    }
    public void PickUpFish()
    {
        GameObject closestFish = null;
        float closestDistance = eatRange;
        float newDistance;
        foreach (GameObject fruit in Fruit)
        {
            newDistance = Vector3.Distance(body.transform.position, fruit.transform.position);
            if (newDistance < eatRange && newDistance < closestDistance)
            {
                closestDistance = newDistance;
                closestFish = fruit;
            }
        }
        if (closestFish != null)
        {
            //destroys the fruit you pick up
            Destroy(closestFish);
            //call function to tick up the counter for eaten fish
            eatenFish += 1f;
            Debug.Log(eatenFish);
        }
    }*/
}
