using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] Enermy;
    public int score=0;
    public TextMeshProUGUI Text;
    void Start()
    {
        StartCoroutine(stopPerSeconde());
    }
    IEnumerator stopPerSeconde()
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0;i<Enermy.Length; i++)
        {
            if (Enermy[i].activeInHierarchy == false)
            {
                Enermy[i].SetActive(true);
                break;
            }
        }
        StartCoroutine(stopPerSeconde());
    }
    public void SetScore()
    {
        score = score + 1;
    }
    // Update is called once per frame
    void Update()
    {
        Text.text = score.ToString();
    }
}
