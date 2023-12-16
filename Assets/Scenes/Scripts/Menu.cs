using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    static float BGMVolume = 1f;
    static float BGMAmbient = 1f;
    static float BGMEffects = 1f;
    static float BGMMaster = 1f;
    public float ScoreFloat;
    private AudioSource backgroundAudioSource, backgroundAmbientAudioSource, castingEffectsAudioSource, menuButtonAudioSource;
    string sceneName;
    static bool firstbootCheck;
    static int normalSouls = 0;
    static int fastSouls = 0;
    static int enemySouls = 0;
    static int normalSouls2x = 0;
    static int fastSouls2x = 0;
    static int enemySouls2x = 0;
    static float endTimer = 0;
    private int roundedScore;
    public Color pressedColor;
    private int maxCharacterLimit = 10;
    private InputField nameInputField;

    void Start()
    {
        GameObject BGMObject = GameObject.Find("BackgroundAudio");
        GameObject BGMAmbientObject = GameObject.Find("BackgroundAmbientAudio");
        GameObject ButtonSoundObject = GameObject.Find("PoppingSound");

        menuButtonAudioSource = ButtonSoundObject.GetComponent<AudioSource>();       

        if(BGMAmbientObject != null)
        {
            //checks if ambientsound exists in the scene and sets volume if so
            backgroundAmbientAudioSource = BGMAmbientObject.GetComponent<AudioSource>();
            SetAmbient();
        }
        if(BGMObject != null)
        {      
            //checks if music exists in the scene and sets volume if so     
            backgroundAudioSource = BGMObject.GetComponent<AudioSource>();
            SetMusic();
        }
        
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if(sceneName == "EndScreen")
        {
            nameInputField = GameObject.Find("Text-input").GetComponent<InputField>();  
            endTimer = FindObjectOfType<Combo>().ReturnTimer();
            SetFishCaught();
            CalculateScore();
            InputFieldListener();
        }
    }
    void OnEnable()
    { 
        //sets bool to true if it's first run and false otherwise
        bool isFirstRun = PlayerPrefs.GetInt("FirstRunv0.12.2", 1) == 1;

        if (isFirstRun)
        {
            // This is the first run of the game.
            BGMMaster = .5f;
            BGMVolume = .5f;
            BGMEffects = .5f;
            BGMAmbient = .5f;
            PlayerPrefs.SetFloat("MasterVolume", BGMMaster);
            PlayerPrefs.SetFloat("MusicVolume", BGMVolume);
            PlayerPrefs.SetFloat("AmbientVolume", BGMAmbient);
            PlayerPrefs.SetFloat("EffectsVolume", BGMEffects);
            PlayerPrefs.SetString("Score1", "");
            PlayerPrefs.SetString("Score2", "");
            PlayerPrefs.SetString("Score3", "");
            PlayerPrefs.SetString("Score4", "");
            PlayerPrefs.SetString("Score5", "");
            PlayerPrefs.SetString("Score6", "");
            PlayerPrefs.SetString("Score7", "");
            PlayerPrefs.SetString("Score8", "");
            PlayerPrefs.SetString("Score9", "");
            PlayerPrefs.SetString("Score10", "");

            PlayerPrefs.SetString("Player1", "");
            PlayerPrefs.SetString("Player2", "");
            PlayerPrefs.SetString("Player3", "");
            PlayerPrefs.SetString("Player4", "");
            PlayerPrefs.SetString("Player5", "");
            PlayerPrefs.SetString("Player6", "");
            PlayerPrefs.SetString("Player7", "");
            PlayerPrefs.SetString("Player8", "");
            PlayerPrefs.SetString("Player9", "");
            PlayerPrefs.SetString("Player10", "");   

            PlayerPrefs.SetString("Norm1", "");
            PlayerPrefs.SetString("Norm2", "");
            PlayerPrefs.SetString("Norm3", "");
            PlayerPrefs.SetString("Norm4", "");
            PlayerPrefs.SetString("Norm5", "");
            PlayerPrefs.SetString("Norm6", "");
            PlayerPrefs.SetString("Norm7", "");
            PlayerPrefs.SetString("Norm8", "");
            PlayerPrefs.SetString("Norm9", "");
            PlayerPrefs.SetString("Norm10", "");

            PlayerPrefs.SetString("Fast1", "");
            PlayerPrefs.SetString("Fast2", "");
            PlayerPrefs.SetString("Fast3", "");
            PlayerPrefs.SetString("Fast4", "");
            PlayerPrefs.SetString("Fast5", "");
            PlayerPrefs.SetString("Fast6", "");
            PlayerPrefs.SetString("Fast7", "");
            PlayerPrefs.SetString("Fast8", "");
            PlayerPrefs.SetString("Fast9", "");
            PlayerPrefs.SetString("Fast10", "");

            PlayerPrefs.SetString("Enemy1", "");
            PlayerPrefs.SetString("Enemy2", "");
            PlayerPrefs.SetString("Enemy3", "");
            PlayerPrefs.SetString("Enemy4", "");
            PlayerPrefs.SetString("Enemy5", "");
            PlayerPrefs.SetString("Enemy6", "");
            PlayerPrefs.SetString("Enemy7", "");
            PlayerPrefs.SetString("Enemy8", "");
            PlayerPrefs.SetString("Enemy9", "");
            PlayerPrefs.SetString("Enemy10", "");

            PlayerPrefs.SetString("Time1", "");
            PlayerPrefs.SetString("Time2", "");
            PlayerPrefs.SetString("Time3", "");
            PlayerPrefs.SetString("Time4", "");
            PlayerPrefs.SetString("Time5", "");
            PlayerPrefs.SetString("Time6", "");
            PlayerPrefs.SetString("Time7", "");
            PlayerPrefs.SetString("Time8", "");
            PlayerPrefs.SetString("Time9", "");
            PlayerPrefs.SetString("Time10", "");
            // Set the flag to indicate it's not the first run next time.
            PlayerPrefs.SetInt("FirstRunv0.12.2", 0);
        }
        else
        {
            // This is not the first run of the game.
            BGMMaster = PlayerPrefs.GetFloat("MasterVolume");
            BGMVolume = PlayerPrefs.GetFloat("MusicVolume");
            BGMEffects = PlayerPrefs.GetFloat("EffectsVolume");
            BGMAmbient = PlayerPrefs.GetFloat("AmbientVolume");
        }

        PlayerPrefs.Save();
        
    }
    public void RefreshStaticMenu()
    {
        normalSouls = 0;
        fastSouls = 0;
        enemySouls = 0;
        normalSouls2x = 0;
        fastSouls2x = 0;
        enemySouls2x = 0;
        endTimer = 0;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("RiverRemastered");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void BackButton()
    {
        SceneManager.LoadScene("NewMenu");
    }
    private void CalculateScore()
    {
        //calculates score on scorescreen then displays it
        string ScoreString;
        TextMeshProUGUI textFieldScore;
        textFieldScore = GameObject.Find("ScoreNum").GetComponent<TextMeshProUGUI>();
        
        ScoreFloat = ((normalSouls*100)+(fastSouls*500)+(enemySouls*200)+(normalSouls2x*200)+(fastSouls2x*1000)+(enemySouls*400)+0.5f*(endTimer*endTimer));
        roundedScore = Mathf.RoundToInt(ScoreFloat);
        ScoreString = "" + roundedScore;
        textFieldScore.text = ScoreString;
    }

    private void SetFishCaught()
    {
        //looks af how many fish were caught and sets value on scorescreen
        //int fishCaught;
        string fishCaughtString2, fastsoulsString, enemyString, timesurvString;
        TextMeshProUGUI textfieldCaught, textfieldFastSoul, textfieldEnemy, textfieldTimeSurv;
        int totalNormal, totalFast, totalEnemies;

        textfieldCaught = GameObject.Find("FishCaughtNum").GetComponent<TextMeshProUGUI>();
        textfieldFastSoul = GameObject.Find("FastSoulNum").GetComponent<TextMeshProUGUI>();
        textfieldEnemy = GameObject.Find("EnemyNum").GetComponent<TextMeshProUGUI>();
        //textfieldCombo = GameObject.Find("ComboMeterNum").GetComponent<TextMeshProUGUI>();
        textfieldTimeSurv = GameObject.Find("TimeSurvivedNum").GetComponent<TextMeshProUGUI>();

        //int roundedCombo = Mathf.RoundToInt(avgCombo*100);
        int roundedTime = Mathf.RoundToInt(endTimer);

        totalNormal = normalSouls + normalSouls2x;
        totalFast = fastSouls + fastSouls2x;
        totalEnemies = enemySouls + enemySouls2x;

        fishCaughtString2 = "" + totalNormal;
        fastsoulsString = "" + totalFast;
        enemyString = "" + totalEnemies;
        //comboString = "" + roundedCombo;
        timesurvString = "" + roundedTime;

        textfieldCaught.text = fishCaughtString2;
        textfieldFastSoul.text = fastsoulsString;
        textfieldEnemy.text = enemyString;
        //textfieldCombo.text = comboString;
        textfieldTimeSurv.text = timesurvString;    
    }

    public void SendStats()
    {
        //this is called when you leave scorescreen to send name that was given to 
        //AddScore function to check if it's a highscore or not then to add to list if so
        Image SubmitButtonImg;
        InputField nameinputField;
        TextMeshProUGUI textfieldNormal, textfieldFast, textfieldEnemy, textfieldTime;
        Button SubmitButton;
        string nameInputString, normalString, fastString, enemyString, timeString;
        nameinputField = GameObject.Find("Text-input").GetComponent<InputField>();
        textfieldNormal = GameObject.Find("FishCaughtNum").GetComponent<TextMeshProUGUI>();
        textfieldFast = GameObject.Find("FastSoulNum").GetComponent<TextMeshProUGUI>();
        textfieldEnemy = GameObject.Find("EnemyNum").GetComponent<TextMeshProUGUI>();
        textfieldTime = GameObject.Find("TimeSurvivedNum").GetComponent<TextMeshProUGUI>();
        SubmitButtonImg = GameObject.Find("SubmitButton").GetComponent<Image>();
        SubmitButton = GameObject.Find("SubmitButton").GetComponent<Button>();
        
        nameInputString = nameinputField.text;
        normalString = textfieldNormal.text;
        fastString = textfieldFast.text;
        enemyString = textfieldEnemy.text;
        timeString = textfieldTime.text;
        //save to highscorescreen
        FindObjectOfType<ScoreBoard>().AddScore(nameInputString, roundedScore, normalString, fastString, enemyString, timeString);
        //will need to send 4 more stats to highscores screen, the 3 souls and time surv
        //disable textfield for name and turn submit button gray
        nameinputField.interactable = false;
        SubmitButton.interactable = false;
        SubmitButtonImg.color = pressedColor;
    }

    public float MasterVolume()
    {
        return BGMMaster;
    }
    public float EffectsVolume()
    {
        return BGMEffects;
    }

    public void MenuButtonSound()
    {
        menuButtonAudioSource.volume = BGMMaster*BGMEffects;
        menuButtonAudioSource.Play();
    }

    public void AddSoul(string soulType)
    {
        //adds souls to static counters to use in score screen
        if(soulType == "NormalSoul")
        {
            //Debug.Log("Added normal soul to counter");
            normalSouls ++;
        }
        else if(soulType == "FastSoul")
        {
            //Debug.Log("Added fast soul to counter");
            fastSouls ++;
        }
        else if(soulType == "Enemy")
        {
            enemySouls ++;
        }
        else if(soulType == "NormalSoul2")
        {
            normalSouls2x ++;
        }
        else if(soulType == "FastSoul2")
        {
            fastSouls2x ++;
        }
        else if(soulType == "Enemy2")
        {
            enemySouls2x ++;
        }
        
    }

    public void UpdateAudio(float newMaster, float newMusic, float newAmbient, float newEffects)
    {
        //this is for when the pause screen is called in the river scene, updates audio
        //Debug.Log("tried to update audio");
        BGMAmbient = newAmbient;
        BGMEffects = newEffects;
        BGMMaster = newMaster;
        BGMVolume = newMusic;

        SetMusic();
        SetAmbient();
    }

    private void SetMusic()
    {
        backgroundAudioSource.volume = BGMVolume*BGMMaster*.5f;
    }

    private void SetAmbient()
    {
        backgroundAmbientAudioSource.volume = BGMAmbient*BGMMaster*.5f;
    }

    private void InputFieldListener()
    {           
        nameInputField.onValueChanged.AddListener(OnNameValueChanged);
    }

    void OnNameValueChanged(string newValue)
    {
        if (newValue.Length > maxCharacterLimit)
        {
            // Truncate the text to the maximum allowed characters
            nameInputField.text = newValue.Substring(0, maxCharacterLimit);
        }

    }

}
