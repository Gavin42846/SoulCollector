using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    TextMeshProUGUI textFieldName1, textFieldName2, textFieldName3, textFieldName4, textFieldName5, textFieldName6, textFieldName7, textFieldName8, textFieldName9, textFieldName10;
    TextMeshProUGUI textFieldScore1, textFieldScore2, textFieldScore3, textFieldScore4, textFieldScore5, textFieldScore6, textFieldScore7, textFieldScore8, textFieldScore9, textFieldScore10;
    TextMeshProUGUI textFieldNormal1, textFieldNormal2, textFieldNormal3, textFieldNormal4, textFieldNormal5, textFieldNormal6, textFieldNormal7, textFieldNormal8, textFieldNormal9, textFieldNormal10;
    TextMeshProUGUI textFieldFast1, textFieldFast2, textFieldFast3, textFieldFast4, textFieldFast5, textFieldFast6, textFieldFast7, textFieldFast8, textFieldFast9, textFieldFast10;
    TextMeshProUGUI textFieldEnemy1, textFieldEnemy2, textFieldEnemy3, textFieldEnemy4, textFieldEnemy5, textFieldEnemy6, textFieldEnemy7, textFieldEnemy8, textFieldEnemy9, textFieldEnemy10;
    TextMeshProUGUI textFieldTimeSurv1, textFieldTimeSurv2, textFieldTimeSurv3, textFieldTimeSurv4, textFieldTimeSurv5, textFieldTimeSurv6, textFieldTimeSurv7, textFieldTimeSurv8, textFieldTimeSurv9, textFieldTimeSurv10;
    static string[] Players;
    static string[] Scores;
    static string[] NormalSouls;
    static string[] FastSouls;
    static string[] EnemySouls;
    static string[] TimeSurvived;
    static float[] ScoresFloat;

    // Start is called before the first frame update

    void Start()
    {
        
        GameObject HighScoresCanvas = GameObject.Find("HighScores");
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if(HighScoresCanvas != null)
        {
            SortArray();
            SetHighScoresText();
        }
        else if(sceneName == "NewMenu")
        {
            Players = new string[10];
            Scores = new string[10];
            ScoresFloat = new float[10];
            NormalSouls = new string[10];
            FastSouls = new string[10];
            EnemySouls = new string[10];
            TimeSurvived = new string[10];
            SetArrayBlank();
            GetHighScores();
            SortArray();               
        }
    }

    /*public void SetName(string name)
    {
        //this function is called in scorescreen by pressing button and will take given data to put in the name array
        Players[1] = name;
        textFieldName1.text = name;
        return;
    }*/

    public void SetHighScoresText()
    {
        //find all names
        textFieldName1 = GameObject.Find("NameText1").GetComponent<TextMeshProUGUI>();
        textFieldName2 = GameObject.Find("NameText2").GetComponent<TextMeshProUGUI>();
        textFieldName3 = GameObject.Find("NameText3").GetComponent<TextMeshProUGUI>();
        textFieldName4 = GameObject.Find("NameText4").GetComponent<TextMeshProUGUI>();
        textFieldName5 = GameObject.Find("NameText5").GetComponent<TextMeshProUGUI>();
        textFieldName6 = GameObject.Find("NameText6").GetComponent<TextMeshProUGUI>();
        textFieldName7 = GameObject.Find("NameText7").GetComponent<TextMeshProUGUI>();
        textFieldName8 = GameObject.Find("NameText8").GetComponent<TextMeshProUGUI>();
        textFieldName9 = GameObject.Find("NameText9").GetComponent<TextMeshProUGUI>();
        textFieldName10 = GameObject.Find("NameText10").GetComponent<TextMeshProUGUI>();
        //find all scores
        textFieldScore1 = GameObject.Find("ScoreText1").GetComponent<TextMeshProUGUI>();
        textFieldScore2 = GameObject.Find("ScoreText2").GetComponent<TextMeshProUGUI>();
        textFieldScore3 = GameObject.Find("ScoreText3").GetComponent<TextMeshProUGUI>();
        textFieldScore4 = GameObject.Find("ScoreText4").GetComponent<TextMeshProUGUI>();
        textFieldScore5 = GameObject.Find("ScoreText5").GetComponent<TextMeshProUGUI>();
        textFieldScore6 = GameObject.Find("ScoreText6").GetComponent<TextMeshProUGUI>();
        textFieldScore7 = GameObject.Find("ScoreText7").GetComponent<TextMeshProUGUI>();
        textFieldScore8 = GameObject.Find("ScoreText8").GetComponent<TextMeshProUGUI>();
        textFieldScore9 = GameObject.Find("ScoreText9").GetComponent<TextMeshProUGUI>();
        textFieldScore10 = GameObject.Find("ScoreText10").GetComponent<TextMeshProUGUI>();
        //find all norms
        textFieldNormal1 = GameObject.Find("SoulsText1").GetComponent<TextMeshProUGUI>();
        textFieldNormal2 = GameObject.Find("SoulsText2").GetComponent<TextMeshProUGUI>();
        textFieldNormal3 = GameObject.Find("SoulsText3").GetComponent<TextMeshProUGUI>();
        textFieldNormal4 = GameObject.Find("SoulsText4").GetComponent<TextMeshProUGUI>();
        textFieldNormal5 = GameObject.Find("SoulsText5").GetComponent<TextMeshProUGUI>();
        textFieldNormal6 = GameObject.Find("SoulsText6").GetComponent<TextMeshProUGUI>();
        textFieldNormal7 = GameObject.Find("SoulsText7").GetComponent<TextMeshProUGUI>();
        textFieldNormal8 = GameObject.Find("SoulsText8").GetComponent<TextMeshProUGUI>();
        textFieldNormal9 = GameObject.Find("SoulsText9").GetComponent<TextMeshProUGUI>();
        textFieldNormal10 = GameObject.Find("SoulsText10").GetComponent<TextMeshProUGUI>();
        //find all fast
        textFieldFast1 = GameObject.Find("FastSoulsText1").GetComponent<TextMeshProUGUI>();
        textFieldFast2 = GameObject.Find("FastSoulsText2").GetComponent<TextMeshProUGUI>();
        textFieldFast3 = GameObject.Find("FastSoulsText3").GetComponent<TextMeshProUGUI>();
        textFieldFast4 = GameObject.Find("FastSoulsText4").GetComponent<TextMeshProUGUI>();
        textFieldFast5 = GameObject.Find("FastSoulsText5").GetComponent<TextMeshProUGUI>();
        textFieldFast6 = GameObject.Find("FastSoulsText6").GetComponent<TextMeshProUGUI>();
        textFieldFast7 = GameObject.Find("FastSoulsText7").GetComponent<TextMeshProUGUI>();
        textFieldFast8 = GameObject.Find("FastSoulsText8").GetComponent<TextMeshProUGUI>();
        textFieldFast9 = GameObject.Find("FastSoulsText9").GetComponent<TextMeshProUGUI>();
        textFieldFast10 = GameObject.Find("FastSoulsText10").GetComponent<TextMeshProUGUI>();
        //find all enemies
        textFieldEnemy1 = GameObject.Find("EnemyText1").GetComponent<TextMeshProUGUI>();
        textFieldEnemy2 = GameObject.Find("EnemyText2").GetComponent<TextMeshProUGUI>();
        textFieldEnemy3 = GameObject.Find("EnemyText3").GetComponent<TextMeshProUGUI>();
        textFieldEnemy4 = GameObject.Find("EnemyText4").GetComponent<TextMeshProUGUI>();
        textFieldEnemy5 = GameObject.Find("EnemyText5").GetComponent<TextMeshProUGUI>();
        textFieldEnemy6 = GameObject.Find("EnemyText6").GetComponent<TextMeshProUGUI>();
        textFieldEnemy7 = GameObject.Find("EnemyText7").GetComponent<TextMeshProUGUI>();
        textFieldEnemy8 = GameObject.Find("EnemyText8").GetComponent<TextMeshProUGUI>();
        textFieldEnemy9 = GameObject.Find("EnemyText9").GetComponent<TextMeshProUGUI>();
        textFieldEnemy10 = GameObject.Find("EnemyText10").GetComponent<TextMeshProUGUI>();
        //find all time
        textFieldTimeSurv1 = GameObject.Find("TimeSurvText1").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv2 = GameObject.Find("TimeSurvText2").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv3 = GameObject.Find("TimeSurvText3").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv4 = GameObject.Find("TimeSurvText4").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv5 = GameObject.Find("TimeSurvText5").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv6 = GameObject.Find("TimeSurvText6").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv7 = GameObject.Find("TimeSurvText7").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv8 = GameObject.Find("TimeSurvText8").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv9 = GameObject.Find("TimeSurvText9").GetComponent<TextMeshProUGUI>();
        textFieldTimeSurv10 = GameObject.Find("TimeSurvText10").GetComponent<TextMeshProUGUI>();

        textFieldName1.text = Players[9];
        textFieldName2.text = Players[8];
        textFieldName3.text = Players[7];
        textFieldName4.text = Players[6];
        textFieldName5.text = Players[5];
        textFieldName6.text = Players[4];
        textFieldName7.text = Players[3];
        textFieldName8.text = Players[2];
        textFieldName9.text = Players[1];
        textFieldName10.text = Players[0];

        textFieldScore1.text = Scores[9];
        textFieldScore2.text = Scores[8];
        textFieldScore3.text = Scores[7];
        textFieldScore4.text = Scores[6];
        textFieldScore5.text = Scores[5];
        textFieldScore6.text = Scores[4];
        textFieldScore7.text = Scores[3];
        textFieldScore8.text = Scores[2];
        textFieldScore9.text = Scores[1];
        textFieldScore10.text = Scores[0];

        textFieldNormal1.text = NormalSouls[9];
        textFieldNormal2.text = NormalSouls[8];
        textFieldNormal3.text = NormalSouls[7];
        textFieldNormal4.text = NormalSouls[6];
        textFieldNormal5.text = NormalSouls[5];
        textFieldNormal6.text = NormalSouls[4];
        textFieldNormal7.text = NormalSouls[3];
        textFieldNormal8.text = NormalSouls[2];
        textFieldNormal9.text = NormalSouls[1];
        textFieldNormal10.text = NormalSouls[0];

        textFieldFast1.text = FastSouls[9];
        textFieldFast2.text = FastSouls[8];
        textFieldFast3.text = FastSouls[7];
        textFieldFast4.text = FastSouls[6];
        textFieldFast5.text = FastSouls[5];
        textFieldFast6.text = FastSouls[4];
        textFieldFast7.text = FastSouls[3];
        textFieldFast8.text = FastSouls[2];
        textFieldFast9.text = FastSouls[1];
        textFieldFast10.text = FastSouls[0];

        textFieldEnemy1.text = EnemySouls[9];
        textFieldEnemy2.text = EnemySouls[8];
        textFieldEnemy3.text = EnemySouls[7];
        textFieldEnemy4.text = EnemySouls[6];
        textFieldEnemy5.text = EnemySouls[5];
        textFieldEnemy6.text = EnemySouls[4];
        textFieldEnemy7.text = EnemySouls[3];
        textFieldEnemy8.text = EnemySouls[2];
        textFieldEnemy9.text = EnemySouls[1];
        textFieldEnemy10.text = EnemySouls[0];

        textFieldTimeSurv1.text = TimeSurvived[9];
        textFieldTimeSurv2.text = TimeSurvived[8];
        textFieldTimeSurv3.text = TimeSurvived[7];
        textFieldTimeSurv4.text = TimeSurvived[6];
        textFieldTimeSurv5.text = TimeSurvived[5];
        textFieldTimeSurv6.text = TimeSurvived[4];
        textFieldTimeSurv7.text = TimeSurvived[3];
        textFieldTimeSurv8.text = TimeSurvived[2];
        textFieldTimeSurv9.text = TimeSurvived[1];
        textFieldTimeSurv10.text = TimeSurvived[0];
    }

    public void AddScore(string name, float score, string normsouls, string fastsouls, string enemysouls, string timesurv)
    {
        if(Scores[9] == "" || Scores[0] == "")
        {
            Players[0] = name;
            Scores[0] = "" + score;
            NormalSouls[0] = normsouls;
            FastSouls[0] = fastsouls;
            EnemySouls[0] = enemysouls;
            TimeSurvived[0] = timesurv;
            SortArray();
            return;
        }
        else if(score > float.Parse(Scores[9]))
        {
            Players[0] = name;
            Scores[0] = "" + score;
            NormalSouls[0] = normsouls;
            FastSouls[0] = fastsouls;
            EnemySouls[0] = enemysouls;
            TimeSurvived[0] = timesurv;
            SortArray();
            return;
        }
        else return;       
    }

    void SortArray()
    {      
        //ADD NEW STATS TO SORT 
        ConvertScores();
        //selection sort algo to sort highscores
        for(int j = 0; j < 10; j++)
        {
            for(int i = 0; i < 9; i++)
            {
            float swapper;
            string nameswapper, scoreswapper, normalswapper, fastswapper, enemyswapper, timeswapper;
            if(ScoresFloat[i] > ScoresFloat[i+1])
            {
                nameswapper = Players[i];
                scoreswapper = Scores[i];
                normalswapper = NormalSouls[i];
                fastswapper = FastSouls[i];
                enemyswapper = EnemySouls[i];
                timeswapper = TimeSurvived[i];
                swapper = ScoresFloat[i];

                Players[i] = Players[i+1];
                Scores[i] = Scores[i+1];
                NormalSouls[i] = NormalSouls[i+1];
                FastSouls[i] = FastSouls[i+1];
                EnemySouls[i] = EnemySouls[i+1];
                TimeSurvived[i] = TimeSurvived[i+1];
                ScoresFloat[i] = ScoresFloat[i+1];


                Players[i+1] = nameswapper;
                Scores[i+1] = scoreswapper;
                NormalSouls[i+1] = normalswapper;
                FastSouls[i+1] = fastswapper;
                EnemySouls[i+1] = enemyswapper;
                TimeSurvived[i+1] = timeswapper;
                ScoresFloat[i+1] = swapper;
            }
            }
        } 
        //saves highscores as playerpref
        SetHighScores(); 
    }
    void ConvertScores()
    {
        //builds the ScoreFloat array from the strings in the Scores array
        for(int i = 9; i >= 0; i--)
        {
            if(Scores[i] == "")
            {
                continue;
            }
            ScoresFloat[i] = float.Parse(Scores[i]);            
        }
    }
    void SetArrayBlank()
    {
        for(int i = 0;i < 10; i++)
        {
            Players[i] = "";
            Scores[i] = "";
            NormalSouls[i] = "";
            FastSouls[i] = "";
            EnemySouls[i] = "";
            TimeSurvived[i] = "";
        }
    }
    void SetHighScores()
    {
        //do player pref stuff here
        PlayerPrefs.SetString("Score1", Scores[9]);
        PlayerPrefs.SetString("Score2", Scores[8]);
        PlayerPrefs.SetString("Score3", Scores[7]);
        PlayerPrefs.SetString("Score4", Scores[6]);
        PlayerPrefs.SetString("Score5", Scores[5]);
        PlayerPrefs.SetString("Score6", Scores[4]);
        PlayerPrefs.SetString("Score7", Scores[3]);
        PlayerPrefs.SetString("Score8", Scores[2]);
        PlayerPrefs.SetString("Score9", Scores[1]);
        PlayerPrefs.SetString("Score10", Scores[0]);

        PlayerPrefs.SetString("Player1", Players[9]);
        PlayerPrefs.SetString("Player2", Players[8]);
        PlayerPrefs.SetString("Player3", Players[7]);
        PlayerPrefs.SetString("Player4", Players[6]);
        PlayerPrefs.SetString("Player5", Players[5]);
        PlayerPrefs.SetString("Player6", Players[4]);
        PlayerPrefs.SetString("Player7", Players[3]);
        PlayerPrefs.SetString("Player8", Players[2]);
        PlayerPrefs.SetString("Player9", Players[1]);
        PlayerPrefs.SetString("Player10", Players[0]);   

        PlayerPrefs.SetString("Norm1", NormalSouls[9]);
        PlayerPrefs.SetString("Norm2", NormalSouls[8]);
        PlayerPrefs.SetString("Norm3", NormalSouls[7]);
        PlayerPrefs.SetString("Norm4", NormalSouls[6]);
        PlayerPrefs.SetString("Norm5", NormalSouls[5]);
        PlayerPrefs.SetString("Norm6", NormalSouls[4]);
        PlayerPrefs.SetString("Norm7", NormalSouls[3]);
        PlayerPrefs.SetString("Norm8", NormalSouls[2]);
        PlayerPrefs.SetString("Norm9", NormalSouls[1]);
        PlayerPrefs.SetString("Norm10", NormalSouls[0]);

        PlayerPrefs.SetString("Fast1", FastSouls[9]);
        PlayerPrefs.SetString("Fast2", FastSouls[8]);
        PlayerPrefs.SetString("Fast3", FastSouls[7]);
        PlayerPrefs.SetString("Fast4", FastSouls[6]);
        PlayerPrefs.SetString("Fast5", FastSouls[5]);
        PlayerPrefs.SetString("Fast6", FastSouls[4]);
        PlayerPrefs.SetString("Fast7", FastSouls[3]);
        PlayerPrefs.SetString("Fast8", FastSouls[2]);
        PlayerPrefs.SetString("Fast9", FastSouls[1]);
        PlayerPrefs.SetString("Fast10", FastSouls[0]);

        PlayerPrefs.SetString("Enemy1", EnemySouls[9]);
        PlayerPrefs.SetString("Enemy2", EnemySouls[8]);
        PlayerPrefs.SetString("Enemy3", EnemySouls[7]);
        PlayerPrefs.SetString("Enemy4", EnemySouls[6]);
        PlayerPrefs.SetString("Enemy5", EnemySouls[5]);
        PlayerPrefs.SetString("Enemy6", EnemySouls[4]);
        PlayerPrefs.SetString("Enemy7", EnemySouls[3]);
        PlayerPrefs.SetString("Enemy8", EnemySouls[2]);
        PlayerPrefs.SetString("Enemy9", EnemySouls[1]);
        PlayerPrefs.SetString("Enemy10", EnemySouls[0]);

        PlayerPrefs.SetString("Time1", TimeSurvived[9]);
        PlayerPrefs.SetString("Time2", TimeSurvived[8]);
        PlayerPrefs.SetString("Time3", TimeSurvived[7]);
        PlayerPrefs.SetString("Time4", TimeSurvived[6]);
        PlayerPrefs.SetString("Time5", TimeSurvived[5]);
        PlayerPrefs.SetString("Time6", TimeSurvived[4]);
        PlayerPrefs.SetString("Time7", TimeSurvived[3]);
        PlayerPrefs.SetString("Time8", TimeSurvived[2]);
        PlayerPrefs.SetString("Time9", TimeSurvived[1]);
        PlayerPrefs.SetString("Time10", TimeSurvived[0]);
    }
    void GetHighScores()
    {
        Scores[9] = PlayerPrefs.GetString("Score1");
        Scores[8] = PlayerPrefs.GetString("Score2");
        Scores[7] = PlayerPrefs.GetString("Score3");
        Scores[6] = PlayerPrefs.GetString("Score4");
        Scores[5] = PlayerPrefs.GetString("Score5");
        Scores[4] = PlayerPrefs.GetString("Score6");
        Scores[3] = PlayerPrefs.GetString("Score7");
        Scores[2] = PlayerPrefs.GetString("Score8");
        Scores[1] = PlayerPrefs.GetString("Score9");
        Scores[0] = PlayerPrefs.GetString("Score10");


        Players[9] = PlayerPrefs.GetString("Player1");
        Players[8] = PlayerPrefs.GetString("Player2");
        Players[7] = PlayerPrefs.GetString("Player3");
        Players[6] = PlayerPrefs.GetString("Player4");
        Players[5] = PlayerPrefs.GetString("Player5");
        Players[4] = PlayerPrefs.GetString("Player6");
        Players[3] = PlayerPrefs.GetString("Player7");
        Players[2] = PlayerPrefs.GetString("Player8");
        Players[1] = PlayerPrefs.GetString("Player9");
        Players[0] = PlayerPrefs.GetString("Player10");

        NormalSouls[9] = PlayerPrefs.GetString("Norm1");
        NormalSouls[8] = PlayerPrefs.GetString("Norm2");
        NormalSouls[7] = PlayerPrefs.GetString("Norm3");
        NormalSouls[6] = PlayerPrefs.GetString("Norm4");
        NormalSouls[5] = PlayerPrefs.GetString("Norm5");
        NormalSouls[4] = PlayerPrefs.GetString("Norm6");
        NormalSouls[3] = PlayerPrefs.GetString("Norm7");
        NormalSouls[2] = PlayerPrefs.GetString("Norm8");
        NormalSouls[1] = PlayerPrefs.GetString("Norm9");
        NormalSouls[0] = PlayerPrefs.GetString("Norm10");

        FastSouls[9] = PlayerPrefs.GetString("Fast1");
        FastSouls[8] = PlayerPrefs.GetString("Fast2");
        FastSouls[7] = PlayerPrefs.GetString("Fast3");
        FastSouls[6] = PlayerPrefs.GetString("Fast4");
        FastSouls[5] = PlayerPrefs.GetString("Fast5");
        FastSouls[4] = PlayerPrefs.GetString("Fast6");
        FastSouls[3] = PlayerPrefs.GetString("Fast7");
        FastSouls[2] = PlayerPrefs.GetString("Fast8");
        FastSouls[1] = PlayerPrefs.GetString("Fast9");
        FastSouls[0] = PlayerPrefs.GetString("Fast10");

        EnemySouls[9] = PlayerPrefs.GetString("Enemy1");
        EnemySouls[8] = PlayerPrefs.GetString("Enemy2");
        EnemySouls[7] = PlayerPrefs.GetString("Enemy3");
        EnemySouls[6] = PlayerPrefs.GetString("Enemy4");
        EnemySouls[5] = PlayerPrefs.GetString("Enemy5");
        EnemySouls[4] = PlayerPrefs.GetString("Enemy6");
        EnemySouls[3] = PlayerPrefs.GetString("Enemy7");
        EnemySouls[2] = PlayerPrefs.GetString("Enemy8");
        EnemySouls[1] = PlayerPrefs.GetString("Enemy9");
        EnemySouls[0] = PlayerPrefs.GetString("Enemy10");

        TimeSurvived[9] = PlayerPrefs.GetString("Time1");
        TimeSurvived[8] = PlayerPrefs.GetString("Time2");
        TimeSurvived[7] = PlayerPrefs.GetString("Time3");
        TimeSurvived[6] = PlayerPrefs.GetString("Time4");
        TimeSurvived[5] = PlayerPrefs.GetString("Time5");
        TimeSurvived[4] = PlayerPrefs.GetString("Time6");
        TimeSurvived[3] = PlayerPrefs.GetString("Time7");
        TimeSurvived[2] = PlayerPrefs.GetString("Time8");
        TimeSurvived[1] = PlayerPrefs.GetString("Time9");
        TimeSurvived[0] = PlayerPrefs.GetString("Time10");
    }
}
