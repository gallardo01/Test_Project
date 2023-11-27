using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondDetection : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private LayerMask diamondLayer;
    [SerializeField] private TextMeshProUGUI score;
    
    private int diamondCount;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("diamondCount")) PlayerPrefs.SetInt("diamondCount", 0);
        diamondCount = PlayerPrefs.GetInt("diamondCount");
        score.SetText(diamondCount.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, player.Direction, out hit, 1f, diamondLayer)) {
            int score = Random.Range(100, 501);
            score = (int) Mathf.Ceil(score / 50.0f) * 50;
            diamondCount += score;
            this.score.SetText(diamondCount.ToString());
            ScoreGenerator.Instance.GenerateScore(score);
            PlayerPrefs.SetInt("diamondCount", diamondCount);
            Destroy(hit.collider.gameObject);
        }
    }
}
