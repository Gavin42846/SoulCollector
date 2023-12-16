using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CatchBar : MonoBehaviour
{
    public float reelPanSpeed = 10f;
    Rigidbody2D reel, fisherBody, reelInBody;
    Vector2 mousePosition, reelPosition;
    public Slider PowerBar, EnemySlider;
    public float chargeMod = 1f;
    private float distanceThrow;
    private TextMeshProUGUI textFieldComboBack, textFieldReelIn, textFieldCast;
    private bool reelOut = false;
    public float castSpeed = 25f;
    private bool catchingFish = false;
    public GameObject CastingSoundObject, StruggleSoundObject, SplashSoundObject, ReelInSoundObject, PoppingSoundObject, FishGetSoundObject;
    public GameObject Note1SoundObject, Note2SoundObject, Note3SoundObject, Note4SoundObject, EnemyGetSoundObject, HeartGetSoundObject;
    public GameObject PauseButtonObject, UnpauseButtonObject, EnemyEatSoundObject;
    private AudioSource castingAudioSource, struggleAudioSource, splashAudioSource, reelInAudioSource, poppingAudioSource, fishGetAudioSource;
    private AudioSource note1AudioSource, note2AudioSource, note3AudioSource, note4AudioSource, enemyGetAudioSource, heartGetAudioSource;
    private AudioSource enemyEatAudioSource;
    private float BGMMaster, BGMEffects;
    public Transform fisherTransform;
    public Button PauseButton, UnpauseButton;
    private float currentTimer, currentUpdateNum, lastUpdateNum;
    public Transform startPointLine; 
    public Transform endPointLine;   
    public LineRenderer lineRenderer;
    public Color lineColor;

    // Start is called before the first frame update
    void Start()
    {
        
        fisherBody = GameObject.Find("Fisherman").GetComponent<Rigidbody2D>();
        reelInBody = GameObject.Find("ReelPoint2").GetComponent<Rigidbody2D>();
        reelInBody.isKinematic = true;
        reel = GameObject.Find("CatchBar").GetComponent<Rigidbody2D>();
        reel.transform.position = new Vector3(0f,-4f,0);
        PowerBar = GameObject.Find("PowerBar").GetComponent<Slider>();
        textFieldComboBack = GameObject.Find("BackgroundCombo").GetComponent<TextMeshProUGUI>();
        textFieldReelIn = GameObject.Find("ReelInIndicator").GetComponent<TextMeshProUGUI>(); 
        textFieldCast = GameObject.Find("CastOutIndicator").GetComponent<TextMeshProUGUI>();  
        castingAudioSource = CastingSoundObject.GetComponent<AudioSource>();  
        struggleAudioSource = StruggleSoundObject.GetComponent<AudioSource>();
        splashAudioSource = SplashSoundObject.GetComponent<AudioSource>();
        reelInAudioSource = ReelInSoundObject.GetComponent<AudioSource>();
        poppingAudioSource = PoppingSoundObject.GetComponent<AudioSource>();
        fishGetAudioSource = FishGetSoundObject.GetComponent<AudioSource>();
        enemyGetAudioSource = EnemyGetSoundObject.GetComponent<AudioSource>();
        heartGetAudioSource = HeartGetSoundObject.GetComponent<AudioSource>();
        note1AudioSource = Note1SoundObject.GetComponent<AudioSource>();
        note2AudioSource = Note2SoundObject.GetComponent<AudioSource>();
        note3AudioSource = Note3SoundObject.GetComponent<AudioSource>();
        note4AudioSource = Note4SoundObject.GetComponent<AudioSource>();
        enemyEatAudioSource = EnemyEatSoundObject.GetComponent<AudioSource>();

        BGMMaster = FindObjectOfType<Menu>().MasterVolume();
        BGMEffects = FindObjectOfType<Menu>().EffectsVolume();
        
        splashAudioSource.volume = BGMMaster*BGMEffects;
        castingAudioSource.volume = BGMMaster*BGMEffects;
        struggleAudioSource.volume = BGMMaster*BGMEffects;
        reelInAudioSource.volume = BGMMaster*BGMEffects;
        poppingAudioSource.volume = BGMMaster*BGMEffects;
        fishGetAudioSource.volume = BGMMaster*BGMEffects;
        enemyGetAudioSource.volume = BGMMaster*BGMEffects;
        heartGetAudioSource.volume = BGMMaster*BGMEffects;
        note1AudioSource.volume = BGMMaster*BGMEffects;
        note2AudioSource.volume = BGMMaster*BGMEffects;
        note3AudioSource.volume = BGMMaster*BGMEffects;
        note4AudioSource.volume = BGMMaster*BGMEffects;
        enemyEatAudioSource.volume = BGMMaster*BGMEffects;

        // Set Line Renderer properties
        lineRenderer.positionCount = 2; // Two points for the line
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;

    }
    // Update is called once per frame
    void Update()
    {
        GetTimer(); 
        PauseEscapeBinding();
        ReelMover(); 
        PowerBarMover();       
        ReelBack();
        PowerBarController();
        ThrowLine();
        textChanger();
        fishingCheck();
        LineHandler();
        
    }
    void LineHandler()
    {
        if (startPointLine != null && endPointLine != null && reel.GetComponent<Renderer>().enabled)
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, startPointLine.position);
            lineRenderer.SetPosition(1, endPointLine.position);
        }
        else 
        {
            lineRenderer.enabled = false;
        }
    }
    void GetTimer()
    {
        currentTimer = FindObjectOfType<Combo>().ReturnTimer();
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
        //Debug.Log("tried to update speed of normal fish");
        lastUpdateNum = currentUpdateNum;
        //gets 10% faster every min
        chargeMod = (1f + roundedMinutes*0.1f);

    }
    void PauseEscapeBinding()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //checks to see if you press escape
        {            
            if(PauseButton != null && PauseButtonObject.activeSelf) //if pause button is active will press button
            {
                PauseButton.onClick.Invoke();
                return;
                             
            }
            if(UnpauseButton != null && UnpauseButtonObject.activeSelf) //if unpause button is active will press button
            {
                UnpauseButton.onClick.Invoke();
                return;
            }
        }
    }
    public void SoundEffectsUpdater(float newMaster, float newEffects)
    {
        //this is for updating the effects volume in the pause menu screen
        BGMMaster = newMaster;
        BGMEffects = newEffects;

        splashAudioSource.volume = BGMMaster*BGMEffects;
        castingAudioSource.volume = BGMMaster*BGMEffects;
        struggleAudioSource.volume = BGMMaster*BGMEffects;
        reelInAudioSource.volume = BGMMaster*BGMEffects;
        poppingAudioSource.volume = BGMMaster*BGMEffects;
        fishGetAudioSource.volume = BGMMaster*BGMEffects;
        enemyGetAudioSource.volume = BGMMaster*BGMEffects;
        heartGetAudioSource.volume = BGMMaster*BGMEffects;
        note1AudioSource.volume = BGMMaster*BGMEffects;
        note2AudioSource.volume = BGMMaster*BGMEffects;
        note3AudioSource.volume = BGMMaster*BGMEffects;
        note4AudioSource.volume = BGMMaster*BGMEffects;
        enemyEatAudioSource.volume = BGMMaster*BGMEffects;
    }
    void PowerBarMover()
    {
        //this makes the power bar follow the fisherman sprite and stay just left of it
        Vector3 newPosition = Camera.main.WorldToScreenPoint(fisherTransform.position); 
        newPosition.x -= 100f;
        PowerBar.transform.position = newPosition;
    }
    void ReelMover()
    {
        //pans at bottom of screen if not casted out
        if(reel.position.y == -4f)
        {
            //turns on bobber indicator when throwing it
            reel.GetComponent<Renderer>().enabled = false;
            reelPosition = fisherBody.position;
            reel.position = reelPosition;         
        }
        else
        {
            //if reeled in all the way turns off bobber indicator
            reel.GetComponent<Renderer>().enabled = true;
            return;
        }
    }
    //throws cast to spot dictated by powerbar
    void ThrowLine()
    {
        if(reel.position.y == -4f && Input.GetKey("space") && (PowerBar.value > 0))
        {
            distanceThrow = ((PowerBar.value) * 8.5f) - 4f;
            reelOut = true;
            //enable reelout sound
            castingAudioSource.Play();
            
            
        }
        //changes velocity to make reel go up when throwing
        if(reelOut == true && reel.position.y < distanceThrow)
        {

            reel.velocity = (new Vector2(0,1))*castSpeed;

        }
        else
        {
            if(reelOut == true)
            {
                //plays splash sound right when reel hits water
                reelOut = false;
                splashAudioSource.Play();
            }
            
            
        }

    }

    void PowerMoverUp()
    {
        //fills powerbar 
        PowerBar.value += (Time.deltaTime * chargeMod);
    }

    void PowerMoverDown()
    {
        //unfills powerbar
        if(PowerBar.value > 0)
        {
            PowerBar.value -= Time.deltaTime * (chargeMod*.66f);
        }
        else return;       
    }
    void DirectionSelector()
    {
        Vector2 velocity = new Vector2(1f, 1f);
        Vector2 fisherPos = reelInBody.position;
        Vector2 reelPos = reel.position;
        Vector2 direction = (fisherPos - reelPos).normalized;
        reel.velocity = direction * reelPanSpeed;
        //reel.transform.Translate(velocity * Time.deltaTime);
    }

    void ReelBack()
    {
        //reels back when player presses down
        if(Input.GetKey("down") && (reel.position.y > -4f))
        {
            //reel.velocity = (new Vector2(0,-1))*reelPanSpeed;
            DirectionSelector();
            if(!ReelInSoundObject.activeSelf)
            {
                ReelInSoundObject.SetActive(true);
            }
        }
        else
        {
            ReelInSoundObject.SetActive(false);
            reel.velocity = (new Vector2(0,0));
        }
        //fixes bug where it reels back too far
        if(reel.position.y < -4f)
        {
            reel.position = new Vector2(reel.position.x, -4f);
        }        
    }

    void PowerBarController()
    {
        //chooses when to charge power bar
        if(Input.GetKey("down") && (reel.position.y == -4f) && catchingFish == false)
        {
            PowerMoverUp();
        }
        else 
        {
            PowerMoverDown();
        }
        //makes powerbar charge 0 if reel is already out
        if(reel.position.y != -4f)
        {
            PowerBar.value = 0f;
        }
        
    }

    void textChanger()
    {
        //updates text to indicate what player can do (reel or casting)
        if(reel.position.y > -4f)
        {
            textFieldReelIn.text = "Press Down(↓) to Reel In!";

        }
        else
        {
            textFieldReelIn.text = "";
        }

        if(reel.position.y == -4f && PowerBar.value > 0)
        {
            textFieldCast.text = "Press Space to Cast Line!";
        }
        else if(reel.position.y == -4f && PowerBar.value == 0 && (textFieldComboBack.text == "") && !EnemySlider.gameObject.activeSelf)
        {
            textFieldCast.text = "Hold Down(↓) to Charge!";
        }
        else
        {
            textFieldCast.text = "";
        }
    }
    //checks if youre fishing by looking at combo text
    void fishingCheck()
    {
        if(textFieldComboBack.text == "" && !EnemySlider.gameObject.activeSelf)
        {
            catchingFish = false;
            //disable fish struggle sound or if already disabled return
            if(StruggleSoundObject.activeSelf)
            {
                StruggleSoundObject.SetActive(false);
            }
        }
        else
        {
            catchingFish = true;
            //either enable fish struggle sound or if already active return
            
            if(StruggleSoundObject.activeSelf)
            {
                return;
            }
            else
            {
                StruggleSoundObject.SetActive(true);

            }
        }
    }

    public void PlayPop()
    {
        poppingAudioSource.Play();
    }

    public void PlayCatchSound()
    {
        fishGetAudioSource.Play();
    }
    public void PlayEnemyCatchSound()
    {
        enemyGetAudioSource.Play();
    }
    public void PlayHeartCatchSound()
    {
        heartGetAudioSource.Play();
    }

    public void PlayNote1()
    {
        note1AudioSource.Play();
    }
    public void PlayNote2()
    {
        note2AudioSource.Play();
    }
    public void PlayNote3()
    {
        note3AudioSource.Play();
    }
    public void PlayNote4()
    {
        note4AudioSource.Play();
    }
    public void EatSoulSound()
    {
        enemyEatAudioSource.Play();
    }
}
