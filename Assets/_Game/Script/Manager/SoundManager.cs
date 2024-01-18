using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public AudioSource MusicAudio;
    public AudioSource SoundAudio;
    [SerializeField] private List<AudioClip> weaponHits;
    [SerializeField] private List<AudioClip> weaponThrows;
    [SerializeField] private List<AudioClip> CharacterDead;

    [SerializeField] private AudioClip ButtonClick;
    [SerializeField] private AudioClip WinAudio;
    [SerializeField] private AudioClip LoseAudio;
    // Start is called before the first frame update
    public bool IsMute;
    void Awake()
    {
        IsMute = false;
        AudioListener.volume = 1;
        this.RegisterListener(EventID.OnEnemyDead, (param) => PlayWeaponHit());
        this.RegisterListener(EventID.ThrowWeapon, (param) => PlayWeaponThrow());
        this.RegisterListener(EventID.Win, (param) => PlayWinMusic());
        this.RegisterListener(EventID.Lose, (param) => PlayLoseMusic());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsMuted()
    {
       return AudioListener.volume == 0;
    }
    public void PlayWinMusic()
    {
        SoundAudio.PlayOneShot(WinAudio);
    }

    public void PlayLoseMusic()
    {
        SoundAudio.PlayOneShot(LoseAudio);

    }
    public void PlayWeaponHit()
    {
        SoundAudio.PlayOneShot(weaponHits[Random.Range(0, weaponHits.Count-1)]);
        //SoundAudio.PlayOneShot(weaponThrows[Random.Range(0, CharacterDead.Count-1)]);
    }

    public void PlayWeaponThrow()
    {
        SoundAudio.PlayOneShot(weaponThrows[Random.Range(0, weaponThrows.Count)]);
    }

    public void ButtonOnClick()
    {
        SoundAudio.PlayOneShot(ButtonClick);
    }
    public void StopSound()
    {
        SoundAudio.Stop();
    }
    public void ChangeVolume()
    {
        bool check = IsMute;
        IsMute = !IsMute;
        AudioListener.volume = IsMute ? 0 : 1;
    }
}
