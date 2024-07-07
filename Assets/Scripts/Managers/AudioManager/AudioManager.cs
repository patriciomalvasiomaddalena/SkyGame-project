using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource),typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializedDictionary("AudioID","AudioClip")]
    [SerializeField] SerializedDictionary<string,AudioClip> SFXDictionary;


    [SerializedDictionary("AudioID", "AudioClip")]
    [SerializeField]SerializedDictionary<string, AudioClip> MusicDictionary;

    [SerializeField] AudioSource[] AudioSources;
 
    private void Awake()
    {
        if(instance == null&& instance != this)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

        AudioSources = GetComponents<AudioSource>();

        if(AudioSources.Length < 1)
        {
            Debug.LogError("AudioManager is missing an Audio Source");
        }
    }

    //get sfx audioclip
    public AudioClip GetSFXAudioClip(string AudioID)
    {
        AudioClip ReturningClip;
        if(SFXDictionary.ContainsKey(AudioID))
        {
           ReturningClip= SFXDictionary[AudioID];
        }
        else
        {
            ReturningClip= null;
            Debug.LogError("Audio Clip ID not found: " + AudioID);
        }
        return ReturningClip;
    }

    //get music audioclip
    public AudioClip GetMusicAudioClip(string AudioID)
    {
        AudioClip ReturningClip;
        if (MusicDictionary.ContainsKey(AudioID))
        {
            ReturningClip = MusicDictionary[AudioID];
        }
        else
        {
            ReturningClip = null;
            Debug.LogError("Audio Clip ID not found: " + AudioID);
        }
        return ReturningClip;
    }

    //play music from the AudioManager, incase We Dont want to save those values
    public void PlayMasterMusicAudio(string AudioID)
    {
        AudioClip ReturningClip;
        if (MusicDictionary.ContainsKey(AudioID))
        {
            ReturningClip = MusicDictionary[AudioID];
            AudioSources[0].clip = ReturningClip;
            AudioSources[0].Play();
        }
        else
        {
            Debug.LogError("ID not Found: " + AudioID);
        }
    }

    //play a SFX from the AudioManager, incase We Dont want to save those values
    public void PlayMasterSfxAudio(string AudioID)
    {
        AudioClip ReturningClip;
        if (SFXDictionary.ContainsKey(AudioID))
        {
            ReturningClip = SFXDictionary[AudioID];
            AudioSources[1].PlayOneShot(ReturningClip);
        }
        else
        {
            Debug.LogError("ID not Found: " + AudioID);
        }
    }

}
