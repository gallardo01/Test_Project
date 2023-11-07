using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameController : MonoBehaviour
{
    public GameObject Enemy; 
    public Transform Star;

    public GameObject[] path;
    public GameObject[] path2;

    public TextMeshProUGUI scoreText; 
    private float score = 0; 
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(spawncharacter), 1f,1f);
    }
    public GameObject[] returnPath() 
    {
        if (Random.Range(0, 2) == 1)
        {
            return path;
        } 
        else
        {
            return path2;
        }
    }

    void spawncharacter()
    {

        GameObject GameController = Instantiate(Enemy, Star.transform.position, Star.transform.rotation); 
    }

    public void increatPoint( int Point)
    {
        score += Point;
        scoreText.text = score.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
