using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    // public static UiManager Instance{
    //     get{
    //         if(instance == null){
    //             instance = FindObjectOfType<UiManager>();
    //         }

    //         return instance;
    //     }
    // }
    [SerializeField] private Text coinText;
    [SerializeField] private float imTime = 0;
    [SerializeField] private float waterTime = 0;
    [SerializeField] private Image shieldImg;
    [SerializeField] private Image waterRunningImg;
    [SerializeField] private Tilemap waterTilemap;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if(imTime > 0){
            imTime -= Time.deltaTime;
        }
        else{
            imTime = 0;
        }
        if(waterTime > 0){
            waterTime -= Time.deltaTime;
            waterTilemap.GetComponent<TilemapCollider2D>().enabled = true;
        }
        else{
            waterTime = 0;
            waterTilemap.GetComponent<TilemapCollider2D>().enabled = false;
        }
        shieldImg.fillAmount = imTime/1.3f;
        waterRunningImg.fillAmount = waterTime/10f;
    }

    public void SetShieldTimer(float imm){
        imTime = imm;
    }

    public void SetWaterTimer(float imm){
        waterTime = imm;
    }

    public void SetCoin(int coin){
        coinText.text = coin.ToString();
    }

}
