using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;


public class LevelManager : Singleton<LevelManager>
{

    [SerializeField] private Player player;
    [SerializeField] private NavMeshData navMeshData;
    public List<Bot> bots = new List<Bot>();    
    public int MaxBot = 3;

    public int CountBotCurrent = 3;
    public int CountBot = 0;

    private void Awake()
    {

        this.RegisterListener(EventID.OnEnemyDead, (param) => OnWeaponHitEnemy((ThrowWeapon)param));

    }
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        
    }

    public void OnInit()
    {
        Name.RandomIndex();
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        player.skinColor.material = ColorManager.Instance.changColor((ColorType)Random.Range(1, 6));
        // bot
        for (int i = 0; i < CountBotCurrent; i++)
        {
            SpawnBot();
        }
        this.PostEvent(EventID.OnPlay);
    }
    private void OnWeaponHitEnemy(ThrowWeapon weapon)
    {

        if (bots.Contains(weapon.Victim))
        {
            //Debug.Log("remove");
            this.bots.Remove(weapon.Victim);
        }
        this.PostEvent(EventID.UpdateAlive, this);
        weapon.Victim.collider.enabled = false;
        this.SpawnEnemyInGame();
        weapon.Victim.changState(weapon.Victim.dead);
        weapon.Remove();

    }
    public Vector3 GetRandomPointOnNavMesh()
    {
        //Vector3 randomPoint = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        Vector3 randomPoint = new Vector3(Random.Range(-20f, 20f), 0f, Random.Range(-20f, 20f));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return randomPoint;
    }

    public string GetCountAlive()
    {
        return (MaxBot - CountBot + bots.Count).ToString();
    }

    public void SpawnBot()
    {
        //Debug.Log("spawn");
        Vector3 pos = GetRandomPointOnNavMesh();
        Bot bot = EasyObjectPool.instance.GetObjectFromPool(EasyObjectPool.instance.poolType[0],pos,Quaternion.identity).GetComponent<Bot>();
        if(bot != null)
        {
            bot.skinColor.material = ColorManager.Instance.changColor((ColorType)Random.Range(1, 6));
            bot.collider.enabled = true;
            if(bot.targetIndicator == null)
            {
                bot.GetTargetIndicator();
            }
            bots.Add(bot);
            bot.OnInit();
            bot.changState(new MoveState());
            CountBot++;
        }
    }
    private void SpawnEnemyInGame()
    {
        if (CountBot >= MaxBot) return;
        if (bots.Count < CountBotCurrent)
        {
            this.SpawnBot();
        }
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
