using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tag.characterTag)
        {
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            CoinUI.Instance.UpdateCoinUI();
        }
    }
}
