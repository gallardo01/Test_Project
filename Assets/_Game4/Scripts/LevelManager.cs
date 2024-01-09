using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int totalBot;
    [SerializeField] private Bot botPrefab;
    public int totalCharacter => totalBot + 1;
    private int totalCharacterAlive;
    [SerializeField] TextMeshProUGUI textAlive;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        // Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", transform.position, Quaternion.identity).GetComponent<Bullet>();
    }

    void OnInit()
    {
        // totalCharacterAlive = totalCharacter;
        Invoke(nameof(InitBots), 1f);
        
    }

    void InitBots(){
        for(int i=0; i<totalBot; i++){
            SpawnBot();
        }
    }

    public void MinusBot(){
        totalBot--;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUi();
    }

    void UpdateUi(){
        totalCharacterAlive = totalCharacter;
        textAlive.text = $"Alive: {totalCharacterAlive}";
    }

    void SpawnBot(){
        Bot bot = EasyObjectPool.instance.GetObjectFromPool("Bot", new Vector3(Random.Range(-20f,20f), 0, Random.Range(-20f,20f)), Quaternion.identity).GetComponent<Bot>();
    }

}
