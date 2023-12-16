using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PickUp : MonoBehaviour
{
    Rigidbody2D body;
    GameObject[] Fruit;
    GameObject[] Enemy;
    GameObject[] Heart;
    GameObject[] FastSoul;
    //GameObject[] Enemy;
    float pickupRange = 0.5f;
    float enemypickupRange = 1f;
    float distanceFloat;
    bool displayBool = false;
    private int FishType = 0;
    private string soulType;

    void Start()
    {
        body = GameObject.Find("CatchBar").GetComponent<Rigidbody2D>();          
    }   
    void Update()
    {
            FruitUpdater();
            EnemyUpdater();
            HeartUpdater();
            FastSoulUpdater();
            //Debug.Log(body.velocity.y);
    }
    void FastSoulUpdater()
    {
        //sets array to how many fruits are on the map

        FastSoul = GameObject.FindGameObjectsWithTag("FastSoul");
        //Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if(GameObject.FindGameObjectsWithTag("FastSoul").Length > 0)
        {           
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("FastSoul").Length; i++)
            {
            if((Vector3.Distance(body.transform.position,FastSoul[i].transform.position) < pickupRange)) //checks to see if fish is close to catch bar
            {
                displayBool = true;
                break;
            }
            displayBool = false;
            }
            if(displayBool == true && (body.velocity.y <= 0f) && (body.position.y > -4f))
            {
                PickUpFastSoul();
                body.position = new Vector2(body.position.x, -4f);
            }
        }

    }
    void PickUpFastSoul()
    {
        GameObject closestFish = null;
        float closestDistance = pickupRange;
        float newDistance;
        foreach (GameObject fastsoul in FastSoul)
        {
            newDistance = Vector3.Distance(body.transform.position, fastsoul.transform.position);
            if (newDistance < pickupRange && newDistance < closestDistance)
            {
                closestDistance = newDistance;
                closestFish = fastsoul;
            }
        }
        if (closestFish != null)
        {
            FishType = 1;
            soulType = "FastSoul";
            
            
            //destroys the fruit you pick up
            Destroy(closestFish);
            //call function to start combo from combo script
            FindObjectOfType<Combo>().ChangeCurrentSoul(soulType);
            FindObjectOfType<Combo>().startCombo(FishType);
            soulType = "";
            FishType = 0;
        }

    }
    void FruitUpdater()
    {
        //sets array to how many fruits are on the map

        Fruit = GameObject.FindGameObjectsWithTag("Fruit");
        //Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if(GameObject.FindGameObjectsWithTag("Fruit").Length > 0)
        {           
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Fruit").Length; i++)
            {
            if((Vector3.Distance(body.transform.position,Fruit[i].transform.position) < pickupRange)) //checks to see if fish is close to catch bar
            {
                displayBool = true;
                break;
            }
            displayBool = false;
            }
            if(displayBool == true && (body.velocity.y <= 0f) && (body.position.y > -4f))
            {
                PickUpFish();
                body.position = new Vector2(body.position.x, -4f);
            }
        }
    }
    
    public void PickUpFish()
    {
        GameObject closestFish = null;
        float closestDistance = pickupRange;
        float newDistance;
        foreach (GameObject fruit in Fruit)
        {
            newDistance = Vector3.Distance(body.transform.position, fruit.transform.position);
            if (newDistance < pickupRange && newDistance < closestDistance)
            {
                closestDistance = newDistance;
                closestFish = fruit;
            }
        }
        if (closestFish != null)
        {
            FishType = 1;
            soulType = "NormalSoul";
            
            
            //destroys the fruit you pick up
            Destroy(closestFish);
            //call function to start combo from combo script
            FindObjectOfType<Combo>().ChangeCurrentSoul(soulType);
            FindObjectOfType<Combo>().startCombo(FishType);
            soulType = "";
            FishType = 0;
        }
    }


    void EnemyUpdater()
    {
        //sets array to how many fruits are on the map

        Enemy = GameObject.FindGameObjectsWithTag("Enemy");
        //Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if(GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {           
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Enemy").Length; i++)
            {
            if((Vector3.Distance(body.transform.position,Enemy[i].transform.position) < enemypickupRange)) //checks to see if fish is close to catch bar
            {
                displayBool = true;
                break;
            }
            displayBool = false;
            }
            if(displayBool == true && (body.velocity.y <= 0f) && (body.position.y > -4f))
            {
                PickUpEnemy();
                body.position = new Vector2(body.position.x, -4f);
            }
        }
    }

    public void PickUpEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = enemypickupRange;
        float newDistance;
        foreach (GameObject enemy in Enemy)
        {
            newDistance = Vector3.Distance(body.transform.position, enemy.transform.position);
            if (newDistance < enemypickupRange && newDistance < closestDistance)
            {
                closestDistance = newDistance;
                closestEnemy = enemy;
            }
        }
        if (closestEnemy != null)
        {
            FishType = 2;
            soulType = "Enemy";
            //destroys the fruit you pick up
            Destroy(closestEnemy);
            //call function to start combo from combo script
            FindObjectOfType<Combo>().ChangeCurrentSoul(soulType);
            FindObjectOfType<Combo>().startCombo(FishType);
            soulType = "";
            FishType = 2;
        }
    }

    void HeartUpdater()
    {
        //sets array to how many fruits are on the map

        Heart = GameObject.FindGameObjectsWithTag("Heart");
        //Enemy = GameObject.FindGameObjectsWithTag("Enemy");

        if(GameObject.FindGameObjectsWithTag("Heart").Length > 0)
        {           
            for(int i = 0; i < GameObject.FindGameObjectsWithTag("Heart").Length; i++)
            {
            if((Vector3.Distance(body.transform.position,Heart[i].transform.position) < pickupRange)) //checks to see if fish is close to catch bar
            {
                displayBool = true;
                break;
            }
            displayBool = false;
            }
            if(displayBool == true && (body.velocity.y <= 0f) && (body.position.y > -4f))
            {
                PickUpHeart();
                body.position = new Vector2(body.position.x, -4f);
            }
        }
    }
    
    public void PickUpHeart()
    {
        GameObject closestHeart = null;
        float closestDistance = pickupRange;
        float newDistance;
        foreach (GameObject heart in Heart)
        {
            newDistance = Vector3.Distance(body.transform.position, heart.transform.position);
            if (newDistance < pickupRange && newDistance < closestDistance)
            {
                closestDistance = newDistance;
                closestHeart = heart;
            }
        }
        if (closestHeart != null)
        {
            FishType = 3;
            soulType = "Heart";
            //destroys the fruit you pick up
            Destroy(closestHeart);
            //call function to start combo from combo script
            FindObjectOfType<Combo>().ChangeCurrentSoul(soulType);
            FindObjectOfType<Combo>().startCombo(FishType);
            soulType = "";
            FishType = 0;
        }
    }

    

}
