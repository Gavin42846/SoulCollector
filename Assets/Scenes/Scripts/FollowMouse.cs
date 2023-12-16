using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FollowMouse : MonoBehaviour
{
    Vector2 mousePosition;
    float yPos;
    Rigidbody2D Fisherman;
    private float fishermanSpeed = 5f;
    private float currentTimer, currentUpdateNum, lastUpdateNum;
    // Start is called before the first frame update
    void Start()
    {
        Fisherman = GameObject.Find("Fisherman").GetComponent<Rigidbody2D>();
        Fisherman.transform.position = new Vector3(0f,-4f,0);
        Fisherman.GetComponent<Renderer>().enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //BearFollow();
        GetTimer(); 
        FisherFollowKeyboardOnly();      
    }

    void BearFollow()
    {
        if(Fisherman.position.y == -4f)
        {
            yPos = Fisherman.position.y;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.y = yPos;
            Fisherman.position = mousePosition;            
        }
        else return;
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
        //gets 10% faster every min
        fishermanSpeed = (5f + roundedMinutes*0.5f);

    }
    void FisherFollowKeyboardOnly()
    {
        if(Input.GetKey("a") && (Fisherman.position.x > -8f))
        {
            Fisherman.velocity = (new Vector2(-1f, 0f))*fishermanSpeed;
        }
        else if(Input.GetKey("d") && (Fisherman.position.x < 8f))
        {
            Fisherman.velocity = (new Vector2(1f, 0f))*fishermanSpeed;
        }
        else Fisherman.velocity = (new Vector2(0f, 0f));
        if(Input.GetKey("d") && Input.GetKey("a"))
        {
            Fisherman.velocity = (new Vector2(0f, 0f));
        }
    }
}
