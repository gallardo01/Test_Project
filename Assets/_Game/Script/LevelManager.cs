using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    private List<Bot> bots = new List<Bot>();
    [SerializeField] private GameObject bot;
    public Player player;
    [SerializeField] int totalBot;
    [SerializeField] GameObject indicatorPrefabs;
    [SerializeField] GameObject canvasIndicator;
    [SerializeField] TextMeshProUGUI textAlive;

    public int TotalCharacter => totalBot + 1;
    private int totalCharacterAlive;
    private static string[] randomName = { "ABC1", "ABC2", "ABC3", "ABC4", "ABC5", "ABC6", "ABC7", "ABC8", "ABC9", "ABC10", "ABC11", "ABC12"};

    void Start()
    {
        totalCharacterAlive = TotalCharacter;
        //OnInit();
    }

    public void OnInit()
    {
        textAlive.text = "Alive: " + totalCharacterAlive.ToString();
        for (int i = 0; i < totalBot; i++)
        {
            NewBot();
        }
    }

    public void InitCharacterAlive()
    {
        totalCharacterAlive--;
        textAlive.text = "Alive: " + totalCharacterAlive.ToString();
    }

    public TargetIndicator CreateIndicatorPanel(Transform target)
    {
        TargetIndicator targetIndicator = Instantiate(indicatorPrefabs, target.transform.position, Quaternion.identity).GetComponent<TargetIndicator>();
        targetIndicator.transform.SetParent(canvasIndicator.transform);
        targetIndicator.OnInit(target.transform);
        targetIndicator.SetInformation(randomName[Random.Range(0, 12)]);
        return targetIndicator;
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
