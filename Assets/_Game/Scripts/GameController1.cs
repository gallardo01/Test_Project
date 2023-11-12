using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController1 : Singleton<GameController1>
{
    // Start is called before the first frame update
    [SerializeField] GameObject enermy;
    [SerializeField] GameObject EnermyParent;
    [SerializeField] GameObject[] Enermy;
    [SerializeField] private GameObject[] SavePoint;
    [SerializeField] private Text TimeWaterRun;
    [SerializeField] private GameObject ImageWaterRun;
    public TilemapCollider2D Water;
    public GameObject drug;
    string s;
    Vector3 x;
    float timeWater;
    GameObject HealClone;
    GameObject DrugClone;
    [SerializeField] private GameObject Heal;
    [SerializeField] private GameObject DrugPotion;
    public GameObject EnermyClone;
    public GameObject HitVFX;
    public int Score = 0;
    public GameObject Key;
    public int stage;
    public bool isOpen = false;
    private void Start()
    {
        Water.enabled = false;
        for (int i = 0; i < Enermy.Length; i++)
        {
            x = SavePoint[UnityEngine.Random.Range(0, SavePoint.Length)].transform.position;
            Enermy[i] = Instantiate(enermy, x , Quaternion.identity);
            Enermy[i].SetActive(false);
            Enermy[i].transform.SetParent(EnermyParent.transform);
        }
        Invoke(nameof(Spawn), 1f);
        if(!PlayerPrefs.HasKey("Stage"))
        {
            PlayerPrefs.SetInt("Stage", 1);
        }
        StartCoroutine(UpdateStage());
    }
    IEnumerator UpdateStage()
    {
        yield return new WaitForSeconds(1f);
        stage = PlayerPrefs.GetInt("Stage");
    }
    public void EnermyDead()
    {
        if (UnityEngine.Random.Range(1, 3) == 1)
        {
            HealClone = Instantiate(Heal, EnermyClone.transform.position, Quaternion.identity);
            HealClone.SetActive(true);
        }

        if (UnityEngine.Random.Range(1, 11) == 1)
        {
            DrugClone = Instantiate(DrugPotion, EnermyClone.transform.position + new Vector3(1f, 0, 0), Quaternion.identity);
            DrugClone.SetActive(true);
        }
        if (UnityEngine.Random.Range(1, 11) == 1 && stage == 2)
        {
            Instantiate(Key, EnermyClone.transform.position + new Vector3(1f, -1f, 0), Quaternion.identity).SetActive(true);
        }
        if (EnermyClone.GetComponent<Enermy>().GetHP() <= 0f) {
            Score++;
        }
        if(Score == 10&& stage == 1)
        {
            PlayerPrefs.SetInt("Stage", 2);
            SceneManager.LoadScene("Loading");
        }
        EnermyClone.SetActive(false);
    }
    public void hitVFX()
    {
        Destroy(HitVFX,0.35f);  
    }
    
    private void Update()
    {
        if(timeWater > 0f)
        {
            timeWater -= Time.deltaTime;
            s = timeWater.ToString();
            TimeWaterRun.text = "" + s[0]+s[1]+s[2];
        }
        if (timeWater <= 0f)
        {
            ImageWaterRun.SetActive(false);
            Water.enabled = false;
        }
    }
    public void Drug()
    {
        timeWater = 10f;
        ImageWaterRun.SetActive(true);
        Water.enabled = true;
        Destroy(drug);
    }
    void Spawn()
    {
        for(int i = 0;i<Enermy.Length;i++){
            if (Enermy[i].activeInHierarchy == false)
            {
                x = SavePoint[UnityEngine.Random.Range(0, SavePoint.Length)].transform.position;
                Enermy[i].transform.position = x;
                Enermy[i].SetActive(true);
                break;
            }
        }
        Invoke(nameof(Spawn), 15f);
    }
    void HoiSinh()
    {

    }
}
