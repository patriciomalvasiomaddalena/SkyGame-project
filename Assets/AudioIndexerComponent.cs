using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIndexerComponent : MonoBehaviour
{
    [SerializeField] string IDAudio;
    [SerializeField] bool IsMusicClip;
    [SerializeField] AudioClip _audioClip;
    [SerializeField] AudioSource _audioSource;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Setup();
    }

    private void Setup()
    {
        if (!IsMusicClip)
        {
            _audioClip = AudioManager.instance?.GetSFXAudioClip(IDAudio);
        }
        else
        {
            _audioClip = AudioManager.instance?.GetMusicAudioClip(IDAudio);
        }

        if(_audioClip != null && _audioSource != null )
        {
            _audioSource.clip = _audioClip;
        }
        else
        {
            Debug.LogError("AudioComponent, missing Audioclip or audioSource");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) 
        {
          AudioManager.instance?.PlayMasterSfxAudio(IDAudio);
        }
    }
}
