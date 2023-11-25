using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public int Score = 0;
    [SerializeField] public TextMeshProUGUI Diamond;
    private void Start()
    {
       PlayerPrefs.SetInt("level", 1);
        if(PlayerPrefs.HasKey("level")==false)
        {
            PlayerPrefs.SetInt("level", 1);
        }
        if (PlayerPrefs.HasKey("Score")==false)
        {
            PlayerPrefs.SetInt("Score", 0);
        }
        Score = PlayerPrefs.GetInt("Score");
    }
    
    // Update is called once per frame

    void Update()
    {
        Diamond.text = Score.ToString();
    }
    public void ChangeScene(int stage)
    {
        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("level", stage);
        SceneManager.LoadScene("level"+ PlayerPrefs.GetInt("level"));
    }
}
