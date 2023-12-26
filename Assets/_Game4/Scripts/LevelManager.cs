using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Bot botPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(InitBots), 1f);
        // Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", transform.position, Quaternion.identity).GetComponent<Bullet>();
    }

    void InitBots(){
        for(int i=0; i<10; i++){
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
