using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AimerIndicator : MonoBehaviour
{
    public Slider PowerBar;
    Vector2 mousePosition;
    Vector2 aimerPosition;
    float yPos;
    float xPos;
    Rigidbody2D aimerBody, fisherBody;
    private float distanceThrow;
    private float fishermanSpeed = 5f;
    private float currentTimer, currentUpdateNum, lastUpdateNum;
    
    // Start is called before the first frame update
    void Start()
    {
        PowerBar = GameObject.Find("PowerBar").GetComponent<Slider>();
        aimerBody = GameObject.Find("AimingReticle").GetComponent<Rigidbody2D>();
        aimerBody.GetComponent<Renderer>().enabled = false;

        fisherBody = GameObject.Find("Fisherman").GetComponent<Rigidbody2D>();
        fisherBody.transform.position = new Vector3(0f,-4f,0);
        fisherBody.GetComponent<Renderer>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        FisherFollowKeyboardOnly();  
        aimerTracker();
        
        
    }
    void aimerTracker()
    {
        //mouse tracker OLD
        /*
        //follows mouse on x axis and powerbar on y axis
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimerPosition.x = mousePosition.x;           
        distanceThrow = ((PowerBar.value) * 8.5f) - 4f;
        aimerPosition.y = distanceThrow;
        aimerBody.position = aimerPosition;*/

        //keyboard tracker NEW
        if(Input.GetKey("a") && (aimerBody.position.x > -8f))
        {
            aimerBody.velocity = (new Vector2(-1f, 0f))*fishermanSpeed;
        }
        else if(Input.GetKey("d") && (aimerBody.position.x < 8f))
        {
            aimerBody.velocity = (new Vector2(1f, 0f))*fishermanSpeed;
        }
        else aimerBody.velocity = (new Vector2(0f, 0f));
        if(Input.GetKey("d") && Input.GetKey("a"))
        {
            aimerBody.velocity = (new Vector2(0f, 0f));
        }
        //adjusts x/y coords of aimer to follow fisher and powerbar
        aimerPosition.x = fisherBody.position.x;
        distanceThrow = ((PowerBar.value) * 8.5f) - 4f;
        aimerPosition.y = distanceThrow;
        aimerBody.position = aimerPosition;

        //displays indicator when powerbar isnt empty
        if(PowerBar.value > 0)
        {
            aimerBody.GetComponent<Renderer>().enabled = true;
            
        }
        else
        {
            aimerBody.GetComponent<Renderer>().enabled = false;
        }
    }
    void FisherFollowKeyboardOnly()
    {
        if(Input.GetKey("a") && (fisherBody.position.x > -8f))
        {
            fisherBody.velocity = (new Vector2(-1f, 0f))*fishermanSpeed;
        }
        else if(Input.GetKey("d") && (fisherBody.position.x < 8f))
        {
            fisherBody.velocity = (new Vector2(1f, 0f))*fishermanSpeed;
        }
        else fisherBody.velocity = (new Vector2(0f, 0f));
        if(Input.GetKey("d") && Input.GetKey("a"))
        {
            fisherBody.velocity = (new Vector2(0f, 0f));
        }
    }

    void UpdateSpeed()
    {
        //round down to see how many min it has been by dividing timer by 60
        float minutesNum = currentTimer/35f;
        int roundedMinutes = Mathf.FloorToInt(minutesNum);
        currentUpdateNum = roundedMinutes;
        //modify stats bases on how many min it has been
        if(roundedMinutes == 0 || (currentUpdateNum == lastUpdateNum))
        {
            return;
        }
        //should only speedup when you hit a new min
        lastUpdateNum = currentUpdateNum;
        //gets 10% faster every min
        fishermanSpeed += (fishermanSpeed*0.15f);

    }
}
