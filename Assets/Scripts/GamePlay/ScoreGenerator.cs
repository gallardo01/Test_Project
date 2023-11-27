using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreGenerator : MonoBehaviour
{

    [SerializeField] private IncreaseScore increaseScore;

    public static ScoreGenerator Instance { get; private set; }

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
        
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    private void Start() {
        increaseScore.Hide();
    }

    public void GenerateScore(int value) {
        increaseScore.Init(value);
    }
}
