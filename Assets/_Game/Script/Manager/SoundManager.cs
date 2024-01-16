using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    public AudioSource SoundAudio;
    [SerializeField] private List<AudioClip> weaponHits;
    [SerializeField] private List<AudioClip> weaponThrows;
    [SerializeField] private List<AudioClip> CharacterDead;
    // Start is called before the first frame update
    public bool IsMute;
    void Start()
    {
        IsMute = false;
        AudioListener.volume = 1;
        this.RegisterListener(EventID.OnEnemyDead, (param) => PlayWeaponHit());
        this.RegisterListener(EventID.ThrowWeapon, (param) => PlayWeaponThrow());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MuteSound()
    {
        AudioListener.volume = 0;
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
}
