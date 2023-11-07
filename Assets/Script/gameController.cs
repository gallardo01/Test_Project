using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{

    [SerializeField] private GameObject botPrefabs;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Text textScore;
    public GameObject[] path1;
    public GameObject[] path2;
    public static Score instance;
    private int score;
    //private void Awake()
    //{
    //    instance = this;
    //}
    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating(nameof(SpawnBot), 1f, 1f);
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void SpawnBot()
    {
        Instantiate(botPrefabs,start.transform.position, Quaternion.identity);
    }
    public GameObject[] returnPath(int path)
    {
        if(path == 1)
        {
            return path1;
        }
        
        return path2;
    }
    public Transform getStart()
    {
        return start;
    }public Transform getEnd()
    {
        return end;
    }

    public void increaseScore()
    {
        score++;
        textScore.text = score.ToString();
    }
}