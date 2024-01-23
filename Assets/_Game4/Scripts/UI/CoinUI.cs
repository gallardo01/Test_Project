using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : Singleton<CoinUI>
{
    [SerializeField] TextMeshProUGUI text;
    public int coinCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", coinCounter);
        }
        coinCounter = PlayerPrefs.GetInt("Coin");
        text.text = coinCounter.ToString();
    }

    public void UpdateCoinUI()
    {
        coinCounter++;
        PlayerPrefs.SetInt("Coin", coinCounter);
        text.text = coinCounter.ToString();
    }
}
