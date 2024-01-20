using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource[] SoundAudioSources;

    private int isMute = 1;
    // Start is called before the first frame update
    void Start()
    {
        // theo game -
        SetVolume(isMute);
    }

    public void SetVolume(int volume)
    {
        for (int i = 0; i < SoundAudioSources.Length; i++)
        {
            SoundAudioSources[i].volume = volume;
            SoundAudioSources[i].mute = volume == 0 ? true : false;
        }
    }

    public void PlayOneShot(int index, float volume)
    {
        if (index < SoundAudioSources.Length)
        {
            // 
            SoundAudioSources[index].volume = volume;
            SoundAudioSources[index].PlayOneShot(SoundAudioSources[index].clip);
        }
    }
    public void PlayOneShot(int index)
    {
        if (index < SoundAudioSources.Length)
        {
            SoundAudioSources[index].PlayOneShot(SoundAudioSources[index].clip);
        }
    }

    public void PlayMusic(int index)
    {
        if (index < SoundAudioSources.Length)
        {
            SoundAudioSources[index].Play();
        }
    }
}
