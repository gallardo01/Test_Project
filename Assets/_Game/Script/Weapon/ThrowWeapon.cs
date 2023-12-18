using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    public Vector3 direct;
    public Character character;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnInit()
    {
        direct =character.target.transform.position -  character.transform.position;
        direct.y = 0;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log(direct);
        this.transform.position += direct * Time.deltaTime;
    }

    public void Remove()
    {
        EasyObjectPool.instance.ReturnObjectToPool(transform.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bot"))
        {
            Destroy(other.gameObject);
            EasyObjectPool.instance.ReturnObjectToPool(this.gameObject);
        }
    }

}
