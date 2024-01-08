using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private List<Bot> bots = new List<Bot>();
    [SerializeField] private GameObject bot;
    public Player player;
    [SerializeField] int totalBot;

    public int TotalCharacter => totalBot + 1;

    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        for (int i = 0; i < totalBot; i++)
        {
            NewBot();
        }
    }

    private void NewBot()
    {
        Bot botSpawn = Instantiate(bot, RandomPoint(), Quaternion.identity).GetComponent<Bot>();
        bots.Add(botSpawn);
    }

    private Vector3 RandomPoint()
    {
        Vector3 randPoint = new Vector3(Random.Range(-15f, 15f), 0.5f, Random.Range(-15f, 15f));
        return randPoint;
    }
}