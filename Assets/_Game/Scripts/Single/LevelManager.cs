using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int botCount;

    [SerializeField] private Character character;
    [SerializeField] private float radius;
    [SerializeField] private GameObject score;
    [SerializeField] private WeaponList weaponList;

    public Transform scoreParent;
    public Transform hintParent;

    public int remainingPlayerCount;

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
        mainCharacter.ChangeWeapon(Random.Range(0, weaponList.Size));

        // Create Bots
        bots = new List<Bot>();

        positions = new Vector3[4];

        Spawn();
    }

    private void Spawn()
    {
        remainingPlayerCount = 1; // Including character

        float x = mainCharacter.transform.position.x;
        float z = mainCharacter.transform.position.z;

        for (int i = 0; i < botCount; i++)
        {

            positions[0] = new Vector3(Random.Range(-radius, -(Constants.attackRange + x)), 0, Random.Range(Constants.attackRange + z, radius));
            positions[1] = new Vector3(Random.Range(Constants.attackRange + x, radius), 0, Random.Range(Constants.attackRange + z, radius));
            positions[2] = new Vector3(Random.Range(Constants.attackRange + x, radius), 0, Random.Range(-(Constants.attackRange + z), -radius));
            positions[3] = new Vector3(Random.Range(-radius, -(Constants.attackRange + x)), 0, Random.Range(-(Constants.attackRange + z), -radius));

            Bot bot = Pools.botPool.Get();
            bot.Pool = Pools.botPool;
            bot.SetScoreText(Instantiate(score, scoreParent));

            bot.ChangeWeapon(Random.Range(0, weaponList.Size));

            bot.transform.position = positions[Random.Range(0, 4)];
            bots.Add(bot);
            remainingPlayerCount++;
        }
    }

    public void OnPlay()
    {
        foreach (Bot bot in bots)
        {
            bot.enabled = true;
        }
        mainCharacter.enabled = true;
    }

    public void OnRetry()
    {
        if (bots.Count == 0) return;
        foreach (Bot bot in bots)
        {
            bot.OnDespawn();
            bot.enabled = false;
        }
        bots.Clear();
        Spawn();

        mainCharacter.transform.position = Vector3.zero;
        mainCharacter.transform.rotation = Quaternion.identity;
        mainCharacter.gameObject.SetActive(true);
        mainCharacter.OnInit();

        OnPlay();


    }

}
