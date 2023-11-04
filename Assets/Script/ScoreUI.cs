using System;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        GameController.OnScoreUpdate += OnScoreUpdate;
    }

    private void OnDestroy()
    {
        GameController.OnScoreUpdate -= OnScoreUpdate;
    }

    void OnScoreUpdate(int score)
    {
        text.text = $"Score: {score}";
    }
}