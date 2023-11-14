using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   public static UIManager instance;
    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if(instance == null)
    //        {
    //            instance = FindObjectOfType<UIManager>();
    //        }
    //        return instance;
    //    }
    //}

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] Text coinText;
    [SerializeField] Text timerText;
    [SerializeField] Text killText;
    [SerializeField] Text kunaiText;
    [SerializeField] GameObject TimeText;
    [SerializeField] private Tilemap tile_water;
    private float timeLeft = 0f;


    private void Start()
    {
        DeActiveTime_text();
        
    }
    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            timeLeft = 0f;
            DeActiveTime_text();
            SetWaterColliderSoft(); 
        }
    }

    private void UpdateTimerDisplay()
    {
        int roundedTime = Mathf.RoundToInt(timeLeft);
        timerText.text = roundedTime.ToString();
    }
    public void setCoin(int coin)
    {
        coinText.text = coin.ToString();
    }
    public void OnInitTime_text()
    {
        TimeText.SetActive(true);
        timeLeft = 10f;
    }

    public void setKill(int kill)
    {
        killText.text = "Kill : " + kill.ToString();
    }
    public void setKunai(int kunai)
    {
        kunaiText.text = "Kunai : " + kunai.ToString();
    }
    public void DeActiveTime_text()
    {
        TimeText.SetActive(false);
    }
    public void SetWaterColliderHard()
    {
        tile_water.GetComponent<CompositeCollider2D>().isTrigger = false;
    }

    public void SetWaterColliderSoft()
    {
        tile_water.GetComponent<CompositeCollider2D>().isTrigger = true;
    }
}
