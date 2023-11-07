using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private void Awake()
    {
        instance = this;
    }
    [SerializeField] Text coinText;
    public void SetCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
}
