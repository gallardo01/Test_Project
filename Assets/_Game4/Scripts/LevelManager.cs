using MarchingBytes;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    private List<Bot> bots = new List<Bot>();
    [SerializeField] private GameObject bot;
    // public Player player;
    public int totalBot;
    [SerializeField] GameObject indicatorPrefabs;
    [SerializeField] private GameObject canvasIndicator;
    private static string[] randomName = { "ABC1", "ABC2", "ABC3", "ABC4", "ABC5", "ABC6", "ABC7", "ABC8", "ABC9", "ABC10", "ABC11", "ABC12" };
    [SerializeField] private TextMeshProUGUI aliveText;
    private int TotalCharacter => totalBot + 1;

    void Start()
    {
        // OnInit();
        // Invoke(nameof(OnInit), 1f);
        UpdateUi();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public TargetIndicator CreateIndicatorPanel(Transform target, Range range)
    {
        TargetIndicator targetIndicator = Instantiate(indicatorPrefabs, target.transform.position, Quaternion.identity).GetComponent<TargetIndicator>();
        targetIndicator.transform.SetParent(canvasIndicator.transform);
        targetIndicator.OnInit(target.transform, range);
        targetIndicator.SetInformation(randomName[Random.Range(0, 12)]);
        return targetIndicator;
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
        // Bot botSpawn = Instantiate(bot, RandomPoint(), Quaternion.identity).GetComponent<Bot>();
        // bots.Add(botSpawn);
        Bot botSpawn = EasyObjectPool.instance.GetObjectFromPool("Bot", RandomPoint(), transform.rotation).GetComponent<Bot>();
    }

    public void UpdateUi()
    {
        aliveText.text = $"Alive: {TotalCharacter}";
    }

    private Vector3 RandomPoint()
    {
        Vector3 randPoint = new Vector3(Random.Range(-15f, 15f), 0.5f, Random.Range(-15f, 15f));
        return randPoint;
    }
}