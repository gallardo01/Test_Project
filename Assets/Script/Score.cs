using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static Score instance;
    public int score;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] Text scoreText;
    public void setScore()
    {
       scoreText.text = score.ToString();
    }

    public void increaseScore()
    {
        score++;
    }
}
