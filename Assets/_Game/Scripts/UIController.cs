using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    int Score = 0;
    Player mainplayer;
    [SerializeField] private TMP_Text ScoreUI, signText;

    // Start is called before the first frame update
    void Start()
    {
        ScoreUI.text = Score + "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpDateScore()
    {
        Score++;
        ScoreUI.text = Score + "";
        
    }

    //win
    public void winSign()
    {
        signText.text = "Congratulations";
    }
    //lose
    public void lostSign()
    {
        signText.text = "What a loser";
    }
}
