using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioSource audioSource;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(audioSource);
        }
        else{
            Destroy(audioSource);
        }

    }
}
