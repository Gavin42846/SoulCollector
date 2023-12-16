using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    private float BGMMaster, BGMVolume, BGMEffects, BGMAmbient;
    private Slider volumeMusicSlider, volumeAmbientSlider, volumeEffectsSlider, volumeMasterSlider;
    void OnEnable()
    {
        BGMMaster = PlayerPrefs.GetFloat("MasterVolume");
        BGMVolume = PlayerPrefs.GetFloat("MusicVolume");
        BGMEffects = PlayerPrefs.GetFloat("EffectsVolume");
        BGMAmbient = PlayerPrefs.GetFloat("AmbientVolume"); 
        volumeMusicSlider = GameObject.Find("MusicVolume").GetComponent<Slider>();
        volumeAmbientSlider = GameObject.Find("AmbientVolume").GetComponent<Slider>();
        volumeEffectsSlider = GameObject.Find("EffectsVolume").GetComponent<Slider>();
        volumeMasterSlider = GameObject.Find("MasterVolume").GetComponent<Slider>();   

        volumeMusicSlider.value = BGMVolume;
        volumeAmbientSlider.value = BGMAmbient;
        volumeEffectsSlider.value = BGMEffects;
        volumeMasterSlider.value = BGMMaster;   
    }

    void OnDisable()
    {
        PlayerPrefs.SetFloat("MasterVolume", BGMMaster);
        PlayerPrefs.SetFloat("MusicVolume", BGMVolume);
        PlayerPrefs.SetFloat("AmbientVolume", BGMAmbient);
        PlayerPrefs.SetFloat("EffectsVolume", BGMEffects);
        
    }

    void Update()
    {
        UpdateSliders();
        FindObjectOfType<Menu>().UpdateAudio(BGMMaster, BGMVolume, BGMAmbient, BGMEffects);
        FindObjectOfType<CatchBar>().SoundEffectsUpdater(BGMMaster, BGMEffects);
    }

    private void UpdateSliders()
    {
        BGMMaster = volumeMasterSlider.value;
        BGMVolume = volumeMusicSlider.value;
        BGMAmbient = volumeAmbientSlider.value;
        BGMEffects = volumeEffectsSlider.value;
    }

    public void RefreshScene()
    {
        FindObjectOfType<Menu>().RefreshStaticMenu();
        FindObjectOfType<Combo>().ResetStaticCombo();

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
