using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuSettings : MonoBehaviour
{
    private TextMeshProUGUI textfieldMaster, textfieldMusic, textfieldAmbient, textfieldEffects;
    private string masterString, musicString, ambientString, effectsString;
    private int masterInt, musicInt, ambientInt, effectsInt;
    static float BGMVolume, BGMAmbient, BGMEffects, BGMMaster;
    private AudioSource backgroundAudioSource, menuButtonAudioSource;
    private Slider volumeMusicSlider, volumeAmbientSlider, volumeEffectsSlider, volumeMasterSlider;
    public Toggle fullscreenToggle;
    void Start()
    {
        BGMEffects = PlayerPrefs.GetFloat("EffectsVolume");
        BGMAmbient = PlayerPrefs.GetFloat("AmbientVolume");
        BGMVolume = PlayerPrefs.GetFloat("MusicVolume");
        BGMMaster = PlayerPrefs.GetFloat("MasterVolume");

        volumeMusicSlider = GameObject.Find("VolumeSliderMusic").GetComponent<Slider>();
        volumeAmbientSlider = GameObject.Find("VolumeSliderAmbient").GetComponent<Slider>();
        volumeEffectsSlider = GameObject.Find("VolumeSliderEffects").GetComponent<Slider>();
        volumeMasterSlider = GameObject.Find("VolumeSliderMaster").GetComponent<Slider>();
        GameObject ButtonSoundObject = GameObject.Find("PoppingSound");

        menuButtonAudioSource = ButtonSoundObject.GetComponent<AudioSource>();

        volumeMusicSlider.value = BGMVolume;
        volumeAmbientSlider.value = BGMAmbient;
        volumeEffectsSlider.value = BGMEffects;
        volumeMasterSlider.value = BGMMaster;

        GameObject BGMObject = GameObject.Find("BackgroundAudio");
        backgroundAudioSource = BGMObject.GetComponent<AudioSource>();
        backgroundAudioSource.volume = BGMVolume*BGMMaster*.5f;

        textfieldMaster = GameObject.Find("VolumeNumMaster").GetComponent<TextMeshProUGUI>();
        textfieldMusic = GameObject.Find("VolumeNumMusic").GetComponent<TextMeshProUGUI>();
        textfieldAmbient = GameObject.Find("VolumeNumAmbient").GetComponent<TextMeshProUGUI>();
        textfieldEffects = GameObject.Find("VolumeNumEffects").GetComponent<TextMeshProUGUI>();

        // Set the initial state of the toggle based on the current fullscreen mode
        fullscreenToggle.isOn = Screen.fullScreen;

        // Add a listener to the toggle to respond to changes
        fullscreenToggle.onValueChanged.AddListener(OnFullscreenToggleChanged);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        BGMMaster = volumeMasterSlider.value;
        BGMVolume = volumeMusicSlider.value;
        backgroundAudioSource.volume = BGMVolume*BGMMaster*.5f;
        BGMAmbient = volumeAmbientSlider.value;
        BGMEffects = volumeEffectsSlider.value;
        menuButtonAudioSource.volume = BGMMaster*BGMEffects;

        masterInt = Mathf.RoundToInt(BGMMaster*100f);
        masterString = (masterInt) + "%";
        textfieldMaster.text = masterString;

        musicInt = Mathf.RoundToInt(BGMVolume*100f);
        musicString = musicInt + "%";
        textfieldMusic.text = musicString;

        ambientInt = Mathf.RoundToInt(BGMAmbient*100f);
        ambientString = ambientInt + "%";
        textfieldAmbient.text = ambientString;

        effectsInt = Mathf.RoundToInt(BGMEffects*100f);
        effectsString = effectsInt + "%";
        textfieldEffects.text = effectsString;
    }
    void OnFullscreenToggleChanged(bool isFullscreen)
    {
        // Change the screen mode based on the toggle state
        Screen.fullScreen = isFullscreen;
    }
    void OnDisable()
    {
        PlayerPrefs.SetFloat("MasterVolume", BGMMaster);
        PlayerPrefs.SetFloat("MusicVolume", BGMVolume);
        PlayerPrefs.SetFloat("AmbientVolume", BGMAmbient);
        PlayerPrefs.SetFloat("EffectsVolume", BGMEffects);
    }
}
