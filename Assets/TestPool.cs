using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPool : MonoBehaviour
{
    Bullet prefab;
    MiniPool<Bullet> pool;
    private void Awake()
    {
        MiniPool<Bullet> pool = new MiniPool<Bullet>();
        pool.OnInit(prefab, 1000);///Tao 1k vien gach o khoang khong


      //  pool.Spawn(postion, rotaion);// Xep tung vien gach vao vi tri minh muon
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }
}
