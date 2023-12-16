using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BearController : MonoBehaviour
{
    private Rigidbody2D body;
    public float runSpeed = 2.0f;
    private float randomX = 0f;
    private float randomY = 0f;
    private float changebehavTimer = -3.0f;
    private float timeKeeper = 0f;
    private bool timerToggle = true;
    public Vector2 velocity;
    private bool valueCheck;
    private float currentTimer, currentUpdateNum, lastUpdateNum;
    
    void Start()
    {
        GetTimer(); 
        UpdateSpeed(); //this is for time
        body = GetComponent<Rigidbody2D>();
        ChangeBehavior();  
             
    }

    private void FixedUpdate()
    {
        GetTimer(); 
        UpdateSpeed();
        if(timerToggle == false){Timer();}       
        if(body.position.x > 10f)
        {
            Destroy(gameObject);
        }
        changebehavTimer += Time.deltaTime;
        if(changebehavTimer >= 0f)
        {
            changebehavTimer -= 3f;
            ChangeBehavior();
        }
        velocity = body.velocity;
        if((body.velocity.magnitude < 0.9f) && timerToggle == true)
        {
            timerToggle = false;
            ChangeBehavior();           
            changebehavTimer = 0f;
            
        }
        

    }
    void GetTimer()
    {
        currentTimer = FindObjectOfType<Combo>().ReturnTimer();
    }
    void UpdateSpeed()
    {
        //round down to see how many min it has been by dividing timer by 60
        float minutesNum = currentTimer/30f;
        int roundedMinutes = Mathf.FloorToInt(minutesNum);
        currentUpdateNum = roundedMinutes;
        //modify stats bases on how many min it has been
        if(roundedMinutes == 0 || (currentUpdateNum == lastUpdateNum))
        {
            return;
        }
        //should only speedup when you hit a new min
        //Debug.Log("tried to update speed of normal fish");
        lastUpdateNum = currentUpdateNum;
        runSpeed = (2f + roundedMinutes*0.2f);

    }

    private void ChangeBehavior()
    {
        //if in center of y axis do regular movement
        if((body.position.y < 1.5f) && (body.position.y > -1.5f))
        {
            //roll random numbers in range for x an y then insert them into body.velocity
            randomX = Random.Range(0.25f, 1f);
            randomY = Random.Range(-0.7f, 0.7f);
            body.velocity = (new Vector2(randomX, randomY)).normalized*runSpeed;            
        }
        else if(body.position.y > 1.5f)
        {
            //make fish go downwards 
            //roll random numbers in range for x an y then insert them into body.velocity
            randomX = Random.Range(0.25f, 1f);
            randomY = Random.Range(-0.7f, 0f);
            body.velocity = (new Vector2(randomX, randomY)).normalized*runSpeed;
            

        }
        else if(body.position.y < -1.5f)
        {
            //make fish go upwards
            randomX = Random.Range(0.25f, 1f);           
            randomY = Random.Range(0f, 0.7f);
            body.velocity = (new Vector2(randomX, randomY)).normalized*runSpeed;
            
        }
        else Debug.Log("None of the 3 'if' statements executed with fish movement.");
        return;       
    }

    private void Timer()
    {
        timeKeeper += Time.deltaTime;
        if(timeKeeper > 1f)
        {
            timeKeeper -= 1f;
            timerToggle = true;
        }
    }

    private void YMultiplier()
    {
        //checks if Y is negative or positive then assigns false if negative and true is positive
        if(randomY < 0f)
        {
            valueCheck = false;
        }
        else valueCheck = true;
        //checks if Y value is less than 0.1 then chenges it to be 0.1 if it is
        if(Mathf.Abs(randomY) < 0.1f)
        {
            if(valueCheck == true)
            {
                randomY = 0.1f;
            }
            else randomY = -0.1f;
        }
        else return;

    }

    private void OnDestroy()
    {
        
    }
}
