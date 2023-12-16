using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Combo : MonoBehaviour
{
    private TextMeshProUGUI textFieldComboBack, textFieldFish, textFieldTimer, textfieldCaught, textfieldLevelTimer, textFieldDirections;
    public TextMeshProUGUI textfield2xIndic;
    bool comboOn = false;
    string comboBackString = "";
    string comboForeString = "";
    int randomGen;
    private float resetTimer, currentSpeed;
    static float levelTimerTotal = 0f;
    private float displayTime = 20f;
    private string displayTimeStr;
    private int fishCaught;
    private int streakCount;
    private string fishCaughtString;
    private string timerString;
    private Slider ComboMeterSlider, SoulVialSlider1, SoulVialSlider2, SoulVialSlider3;
    public Slider EnemySlider;
    string sceneName;
    private float comboDrain = 0.01f;
    public Color lowcomboColor, mediumcomboColor, highcomboColor, comboResetColor;
    private Image combofillImage;
    private GameObject BGMObject;
    private GameObject HeartBack1, HeartBack2, HeartBack3;
    private AudioSource BGMAudioSource;
    private bool slowMo;
    private float timeMod = 0.35f;
    private int currentHearts = 3;
    private Image heartBack1Img, heartBack2Img, heartBack3Img;
    private int comboAmount = 4;
    private bool enemyCombo = false;
    private string currentSoul;
    private bool isPaused = false;
    public Color heartRed, heartBlack;
    private float currentTimer, currentUpdateNum, lastUpdateNum, sliderDrain;
    private bool doublePoints = false;
    private int comboCounter = 0;
    private string displayCombo = "";
    private int startIndex;
    private int lengthToReplace = 6;
    private string updatecolorString;

    
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if(sceneName == "RiverRemastered")
        {          
            GetAll();
            sliderDrain = 0.3f;
            levelTimerTotal = 0f;
            comboCounter = 0;
            textFieldComboBack.text = "";
            resetTimer = displayTime; 
            HeartUpdater();

        }             
    }

    // Update is called once per frame
    void Update()
    {
        
        if(sceneName == "RiverRemastered")
        {
            if(!isPaused)
            {
                UpdateComboNums();
                SlowMotionControl();
                ComboSliderHandler();
                ComboCheck();  
                LevelTimer(); 
                EnemySliderHandler();
            }
            
        }       
    }
    public float ReturnTimer()
    {
        return levelTimerTotal;
    }

    void UpdateComboNums()
    {
        currentTimer = levelTimerTotal;
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
        comboAmount++;
        resetTimer -= resetTimer*0.15f;
        sliderDrain += sliderDrain*0.20f;
        lastUpdateNum = currentUpdateNum;       
        //put thing here to change color of enemy when this happens
    }
    void GetAll()
    {
        HeartBack1 = GameObject.Find("Heart1Back");
        HeartBack2 = GameObject.Find("Heart2Back");
        HeartBack3 = GameObject.Find("Heart3Back");
        
        heartBack1Img = HeartBack1.GetComponent<Image>();
        heartBack2Img = HeartBack2.GetComponent<Image>();
        heartBack3Img = HeartBack3.GetComponent<Image>();

        BGMObject = GameObject.Find("BackgroundAudio");
        BGMAudioSource = BGMObject.GetComponent<AudioSource>();
        ComboMeterSlider = GameObject.Find("ComboMeter").GetComponent<Slider>();
        SoulVialSlider1 = GameObject.Find("SoulVialSlider1").GetComponent<Slider>();
        SoulVialSlider2 = GameObject.Find("SoulVialSlider2").GetComponent<Slider>();
        SoulVialSlider3 = GameObject.Find("SoulVialSlider3").GetComponent<Slider>();
        combofillImage = ComboMeterSlider.fillRect.GetComponent<Image>();
        textfieldLevelTimer = GameObject.Find("LevelTimer").GetComponent<TextMeshProUGUI>();
        textFieldComboBack = GameObject.Find("BackgroundCombo").GetComponent<TextMeshProUGUI>();
        textFieldTimer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        textFieldDirections = GameObject.Find("DirectionsTxt").GetComponent<TextMeshProUGUI>();

    }
    public void ResetStaticCombo()
    {
        levelTimerTotal = 0;
    }

    public void KillHeart()
    {
        //checks HP and updates hearts
        if(currentHearts == 1)
        {
            SceneManager.LoadScene("EndScreen");
            
            //go to score screen
            return;
        }
        if(currentHearts == 5)
        {
            currentHearts -= 1;
            HeartUpdater();
            return;
        }
        if(currentHearts == 4)
        {
            currentHearts -= 1;
            HeartUpdater();
            return;
        }
        if(currentHearts == 3)
        {
            currentHearts -= 1;
            HeartUpdater();
            return;
        }
        if(currentHearts == 2)
        {
            currentHearts -= 1;
            HeartUpdater();
            return;
        }
    }

    private void HeartUpdater()
    {
        if(currentHearts > 0)
        {
            //make heart red on these
            heartBack1Img.color = heartRed;
        }
        if(currentHearts > 1)
        {
            heartBack2Img.color = heartRed;
        }
        else
        {
            heartBack2Img.color = heartBlack;
        }
        if(currentHearts > 2)
        {
            heartBack3Img.color = heartRed;
        }
        else
        {
            heartBack3Img.color = heartBlack;           
        }
        if(currentHearts > 3)
        {
            currentHearts = 3;
        }
    }

    public void HealHeart()
    {
        if(currentHearts < 5)
        {
            currentHearts += 1;
            HeartUpdater();
        }
    }

    private void SlowMotionControl()
    {
        if(isPaused)
        {
            return;
        }
        if(comboOn || enemyCombo)
        {
            slowMo = true;
            Time.timeScale = timeMod;
            //BGMAudioSource.pitch = 0.97f;
        }
        else
        {
            slowMo = false;
            if(!isPaused)
            {
                Time.timeScale = 1f;
            }           
            //BGMAudioSource.pitch = 1f;
        }

    }

    public void PauseOn()
    {
        currentSpeed = Time.timeScale;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void PauseOff()
    {
        Time.timeScale = currentSpeed;
        isPaused = false;
    }

        
    private void ComboSliderHandler()
    {
        //drain if not double points
        if(!doublePoints)
        {
            if(ComboMeterSlider.value < .33)
            {
            combofillImage.color = lowcomboColor;
            ComboMeterSlider.value -= (Time.deltaTime*comboDrain);
            //make the value of SoulVial1 be 3* combometer
            SoulVialSlider1.value = ComboMeterSlider.value*3f;
            SoulVialSlider2.value = 0f;
            SoulVialSlider3.value = 0f;

            }
            else if(ComboMeterSlider.value < .66)
            {
                combofillImage.color = mediumcomboColor;
                ComboMeterSlider.value -= (Time.deltaTime*comboDrain*1.25f);
                //make vial 1 max and make vial 2 value be combometer -.33 *3
                SoulVialSlider1.value = 1f;
                SoulVialSlider2.value = (ComboMeterSlider.value - .33f)*3f;
                SoulVialSlider3.value = 0f;
            }
            else
            {
                combofillImage.color = highcomboColor;
                ComboMeterSlider.value -= (Time.deltaTime*comboDrain*2f);
                //make vial 1/2 max and vial 3 is combometer -.66 *3
                SoulVialSlider1.value = 1f;
                SoulVialSlider2.value = 1f;
                SoulVialSlider3.value = (ComboMeterSlider.value - .66f)*3f;
                if(ComboMeterSlider.value >= .99f)
                {
                    //Debug.Log("tries to turn on double points");
                    doublePoints = true;
                    textfield2xIndic.text = "2x Points!";
                    //turn on 2x mode and stop combo from giving combo till drain and make drain 5x
                }
            } 
        }
        else
        {
            if(ComboMeterSlider.value <= 0f)
            {
                doublePoints = false;
                textfield2xIndic.text = "";
            }
            else
            {
                if(ComboMeterSlider.value < .33)
                {
                combofillImage.color = lowcomboColor;
                ComboMeterSlider.value -= (Time.deltaTime*comboDrain*5);
                //make the value of SoulVial1 be 3* combometer
                SoulVialSlider1.value = ComboMeterSlider.value*3f;
                SoulVialSlider2.value = 0f;
                SoulVialSlider3.value = 0f;

                }
                else if(ComboMeterSlider.value < .66)
                {
                    combofillImage.color = mediumcomboColor;
                    ComboMeterSlider.value -= (Time.deltaTime*comboDrain*5f);
                    //make vial 1 max and make vial 2 value be combometer -.33 *3
                    SoulVialSlider1.value = 1f;
                    SoulVialSlider2.value = (ComboMeterSlider.value - .33f)*3f;
                    SoulVialSlider3.value = 0f;
                }
                else
                {
                    combofillImage.color = highcomboColor;
                    ComboMeterSlider.value -= (Time.deltaTime*comboDrain*5f);
                    //make vial 1/2 max and vial 3 is combometer -.66 *3
                    SoulVialSlider1.value = 1f;
                    SoulVialSlider2.value = 1f;
                    SoulVialSlider3.value = (ComboMeterSlider.value - .66f)*3f;
                    if(ComboMeterSlider.value >= .99f)
                    {
                        //Debug.Log("tries to turn on double points");
                        doublePoints = true;
                        //turn on 2x mode and stop combo from giving combo till drain and make drain 5x
                    }
                }
            }            
        }
              
    }
    public void startCombo(int FishType)
    {
        if(FishType == 1)
        {
            comboBackString = "";
            textFieldDirections.text = "Press Directional Arrows to Catch!";
            //make for loop that loops x amount of times for each number set to a letter
            for(int i = 0; i < comboAmount; i++)
            {
                randomGen = Random.Range(0, 4);
                switch (randomGen)
                {
                    case 0:
                        displayCombo += "<color=#0000FF>\u2190</color>";
                        comboBackString += "←";
                        break;
                    case 1:
                        displayCombo += "<color=#008000>\u2193</color>";
                        comboBackString += "↓";
                        break;
                    case 2:
                        displayCombo += "<color=#FF0000>\u2192</color>";
                        comboBackString += "→";
                        break;
                    case 3:
                        displayCombo += "<color=#FFFF00>\u2191</color>";
                        comboBackString += "↑";
                        break;

                }
            }
            textFieldComboBack.text = displayCombo;
            comboOn = true;
        }
        else if (FishType == 2)
        {
            textFieldDirections.text = "Mash Down Arrow (↓) to Catch!";
            enemyCombo = true;
        }
        else if (FishType == 3)
        {
            HealHeart();
            FindObjectOfType<CatchBar>().PlayHeartCatchSound();
            //play heart pickup sound
        }       
    }

    void ComboColorUpdater()
    {
        //8-13 change to new color hex E0E0E0
        string newColor = "E0E0E0";
        if(comboCounter == 1)
        {
            startIndex = 8*comboCounter;
            if (startIndex + lengthToReplace <= displayCombo.Length)
            {
                // Create the modified string
                updatecolorString =
                    displayCombo.Substring(0, startIndex) + // Before the portion to replace
                    newColor +
                    displayCombo.Substring(startIndex + lengthToReplace); // After the portion to replace
                    
                textFieldComboBack.text = updatecolorString;
            }
        }
        else
        {
            startIndex = (8+((comboCounter-1)*24));
            if (startIndex + lengthToReplace <= updatecolorString.Length)
            {
                // Create the modified string
                updatecolorString =
                    updatecolorString.Substring(0, startIndex) + // Before the portion to replace
                    newColor +
                    updatecolorString.Substring(startIndex + lengthToReplace); // After the portion to replace
                    
                textFieldComboBack.text = updatecolorString;
            }
        }         
    }

    void ClearCombo()
    {
        displayCombo = "";
        updatecolorString = "";
        comboCounter = 0;
        textFieldTimer.text = "";
        displayTime = resetTimer;       
        comboBackString = "";
        comboForeString = "";
        textFieldComboBack.text = "";
        textFieldDirections.text = "";
        comboOn = false;
        enemyCombo = false;          
    }

    void PassCombo()
    {
        if(currentSoul == "NormalSoul" && !doublePoints)
        {
            ComboMeterSlider.value += 0.1f;
            FindObjectOfType<CatchBar>().PlayCatchSound();
            
        }
        else if(currentSoul == "FastSoul" && !doublePoints)
        {
            ComboMeterSlider.value += 0.25f;
            FindObjectOfType<CatchBar>().PlayCatchSound();
           
        }
        else if(currentSoul == "Enemy" && !doublePoints)
        {
            EnemySlider.gameObject.SetActive(false);
            ComboMeterSlider.value += 0.15f;
            FindObjectOfType<CatchBar>().PlayEnemyCatchSound();
            //play enemy catch sound
            
        }   
        else if(currentSoul == "NormalSoul" && doublePoints)
        {
            
            FindObjectOfType<CatchBar>().PlayCatchSound();
            //call function that adds a double points fish to counter
            currentSoul += "2";
            
        }
        else if(currentSoul == "FastSoul" && doublePoints)
        {
            
            FindObjectOfType<CatchBar>().PlayCatchSound();
            currentSoul += "2";
           
        }
        else if(currentSoul == "Enemy" && doublePoints)
        {
            EnemySlider.gameObject.SetActive(false);
            
            FindObjectOfType<CatchBar>().PlayEnemyCatchSound();
            //play enemy catch sound
            currentSoul += "2";
            
        }   
        FindObjectOfType<Menu>().AddSoul(currentSoul);       
        currentSoul = "";
    }

    void ComboCheck()
    {
        if(comboOn == true)
        {
            //if wrong inputs
            if(comboBackString[comboForeString.Length] == '←' && (Input.GetKeyDown("down") || Input.GetKeyDown("up") || Input.GetKeyDown("right")))
            {
                //Debug.Log("wrong button press");
                ComboMeterSlider.value -= ComboMeterSlider.value*0.1f;
                displayTime -= displayTime*0.2f;
            }
            else if(comboBackString[comboForeString.Length] == '↓' && (Input.GetKeyDown("left") || Input.GetKeyDown("up") || Input.GetKeyDown("right")))
            {
                //Debug.Log("wrong button press");
                ComboMeterSlider.value -= (ComboMeterSlider.value*0.1f);
                displayTime -= displayTime*0.2f;
            }
            else if(comboBackString[comboForeString.Length] == '→' && (Input.GetKeyDown("left") || Input.GetKeyDown("up") || Input.GetKeyDown("down")))
            {
                //Debug.Log("wrong button press");
                ComboMeterSlider.value -= (ComboMeterSlider.value*0.1f);
                displayTime -= displayTime*0.2f;
            }
            else if(comboBackString[comboForeString.Length] == '↑' && (Input.GetKeyDown("left") || Input.GetKeyDown("down") || Input.GetKeyDown("right")))
            {
                //Debug.Log("wrong button press");
                ComboMeterSlider.value -= (ComboMeterSlider.value*0.1f);
                displayTime -= displayTime*0.2f;
            }
            //if correct inputs
            if(Input.GetKeyDown("left") && comboBackString[comboForeString.Length] == '←')
            {
                comboCounter ++;
                comboForeString += "←";
                //put combocolor changer here
                ComboColorUpdater();
                FindObjectOfType<CatchBar>().PlayNote1();
                //play A note
            }
            else if(Input.GetKeyDown("down") && comboBackString[comboForeString.Length] == '↓')
            {
                comboCounter ++;
                comboForeString += "↓";
                //put combocolor changer here
                ComboColorUpdater();
                FindObjectOfType<CatchBar>().PlayNote2();
                //play B note
            }
            else if(Input.GetKeyDown("right") && comboBackString[comboForeString.Length] == '→')
            {
                comboCounter ++;
                comboForeString += "→";
                //put combocolor changer here
                ComboColorUpdater();
                FindObjectOfType<CatchBar>().PlayNote3();
                //play C note
            }
            else if(Input.GetKeyDown("up") && comboBackString[comboForeString.Length] == '↑')
            {
                comboCounter ++;
                comboForeString += "↑";
                //put combocolor changer here
                ComboColorUpdater();
                FindObjectOfType<CatchBar>().PlayNote4();
                //play D note
            }
            //textFieldCombo.text = comboForeString;
            Timer();
        
        }
        //clears combo once foreground=background
        if(comboBackString == comboForeString && comboBackString != "")
        {
            PassCombo();
            ClearCombo();
        }
    }

    void Timer()
    {
        if(displayTime < 0)
        {
            KillHeart();
            ClearCombo();
            return;
        }
        if(slowMo)
        {
            displayTime -= Time.deltaTime*(1/timeMod);
        }
        else
        {
            displayTime -= Time.deltaTime;
        }
        
        if(displayTime >= 10)
        {
            displayTimeStr = string.Format("{0:00}", displayTime);
        }
        else displayTimeStr = string.Format("{0:0}", displayTime);
        textFieldTimer.text = displayTimeStr;


    }

    private void LevelTimer()
    {
        if(slowMo)
        {
            levelTimerTotal += Time.deltaTime*(1/timeMod);
        }
        else
        {
            levelTimerTotal += Time.deltaTime;
        }
        
        timerString = string.Format("{0:0}", levelTimerTotal);
        textfieldLevelTimer.text = timerString;
    }

    public void ChangeCurrentSoul(string soulType)
    {
        //Debug.Log("got to double and statement");
        if(!comboOn && !enemyCombo)
        {
           // Debug.Log("tried to swap fish type");
            currentSoul = soulType;
        }
        
    }

    private void EnemySliderHandler()
    {
        if(enemyCombo)
        {    
            Timer();
            if(!EnemySlider.gameObject.activeSelf)   
            {
                EnemySlider.gameObject.SetActive(true);

            }  
            EnemySlider.value -= Time.deltaTime*sliderDrain;
            if(Input.GetKeyDown("down"))
            {
                EnemySlider.value += 0.15f;
            }
            if(EnemySlider.value == 1f)
            {
                EnemySlider.value = 0f;
                PassCombo();
                ClearCombo();
                return;
            }
        }
        else
        {
            EnemySlider.gameObject.SetActive(false);
        }
    }
    


}
