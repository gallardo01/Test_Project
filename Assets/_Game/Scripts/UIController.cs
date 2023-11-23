using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    int Score = 0;
    [SerializeField] private TMP_Text ScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpDateScore()
    {
        Score++;
    }

    public void UpDateUI()
    {
        ScoreUI.text = Score + "";
    }
}
