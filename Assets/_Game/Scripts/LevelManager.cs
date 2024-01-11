using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int botCount;

    [SerializeField] private Character character;
    [SerializeField] private float radius;
    [SerializeField] private GameObject score;

    public Transform scoreParent;
    public Transform hintParent;
    
    public int remainingBotCount;

    private List<Bot> bots;
    private Vector3[] positions;
    private Character mainCharacter;

    public static LevelManager Instance { get; private set; }
    public List<Bot> Bots => bots;
    public Character MainCharacter => mainCharacter;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        // Create Character
        mainCharacter = Instantiate(character, Vector3.zero, Quaternion.identity, null);
        mainCharacter.enabled = false;
        mainCharacter.SetScoreText(Instantiate(score, scoreParent));

        // Create Bots
        bots = new List<Bot>();
        
        positions = new Vector3[4];

        remainingBotCount = 1; // Including character

        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < botCount; i++)
        {
            Bot bot = Pools.botPool.Get();
            bot.Pool = Pools.botPool;
            bot.SetScoreText(Instantiate(score, scoreParent));
            bot.Character = mainCharacter.transform;

            float x = mainCharacter.transform.position.x;
            float z = mainCharacter.transform.position.z;

            positions[0] = new Vector3(Random.Range(-radius, -(bot.attackRange + x)), 0, Random.Range(bot.attackRange + z, radius));
            positions[1] = new Vector3(Random.Range(bot.attackRange + x, radius), 0, Random.Range(bot.attackRange + z, radius));
            positions[2] = new Vector3(Random.Range(bot.attackRange + x, radius), 0, Random.Range(-(bot.attackRange + z), -radius));
            positions[3] = new Vector3(Random.Range(-radius, -(bot.attackRange + x)), 0, Random.Range(-(bot.attackRange + z), -radius));

            bot.transform.position = positions[Random.Range(0, 4)];
            bots.Add(bot);
            remainingBotCount++;
        }
    }

    public void OnPlay() {
        foreach (Bot bot in bots) {
            bot.enabled = true;
        }
        mainCharacter.enabled = true;
    }

}
