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
        totalCharacterAlive = totalCharacter;
        Invoke(nameof(InitBots), 1f);
        // Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", transform.position, Quaternion.identity).GetComponent<Bullet>();
    }

    void OnInit()
    {
        
    }

    void InitBots(){
        for(int i=0; i<totalBot; i++){
            SpawnBot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBot(){
        Bot bot = EasyObjectPool.instance.GetObjectFromPool("Bot", new Vector3(Random.Range(-20f,20f), 0, Random.Range(-20f,20f)), Quaternion.identity).GetComponent<Bot>();
    }

}
