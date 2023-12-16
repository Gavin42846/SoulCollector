using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatTrack : MonoBehaviour
{
    private TextMeshProUGUI textfieldCaught;

    private int fishCaught;
    private int streakCount;
    private string fishCaughtString;

    // Start is called before the first frame update
    void Start()
    {
        textfieldCaught = GameObject.Find("FishCaughtNum").GetComponent<TextMeshProUGUI>();
        //fishCaught = FindObjectOfType<Combo>().returnFish();
        fishCaughtString = "" + fishCaught;
        SetFishCaught();
    }

    void SetFishCaught()
    {
        textfieldCaught.text = fishCaughtString;
    }

}
