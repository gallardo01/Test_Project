using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    // [SerializeField] private Image shieldImg;

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
        // shieldImg.fillAmount = imTime;
    }

    public void DisableShield(float imm){
        imTime = imm;
    }

    public void SetCoin(int coin){
        coinText.text = coin.ToString();
    }

}
