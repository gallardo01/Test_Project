using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public struct BasePoint
{
    public int Score;
    public float Scale;
     public int DeadScore;
}
public class LevelManager : Singleton<LevelManager>
{
    public List<Weapon> weapons = new List<Weapon>();
    public Player player;
    [SerializeField] private NavMeshData navMeshData;
    public List<BasePoint> basePoints = new List<BasePoint>();
    public List<Bot> bots = new List<Bot>();    
    public int MaxBot = 3;
    
    public int CountBotCurrent = 3;
    public int CountBot = 0;

    private void Awake()
    {

        this.RegisterListener(EventID.OnEnemyDead, (param) => OnWeaponHitEnemy((ThrowWeapon)param));

    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    OnInit();

    //}

    public void OnInit()
    {
        bots.Clear();
        CountBot = 0;
        Name.RandomIndex();
        InitPointScale();
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
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
            this.bots.Remove(weapon.Victim);
        }
        this.PostEvent(EventID.UpdateAlive, this);
        weapon.Victim.collider.enabled = false;
        this.SpawnEnemyInGame();
        weapon.character.UpdateScore(weapon.Victim.deadScore);
        weapon.character.GrowthCharacter();       
        weapon.Victim.changState(weapon.Victim.dead);
        weapon.OnDespawn();
        if (bots.Count == 0)
        {
            this.PostEvent(EventID.Win);
        }
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
        return (MaxBot - CountBot + bots.Count + 1).ToString();
    }
    public int GetRank()
    {
        return (MaxBot - CountBot + bots.Count +1);
    }

    public int RandomPoint()
    {
        int point = Random.Range(player.score - 3, player.score + 3);
        if (point <= 0) point = 1;
        return point;
    }
    public void SpawnBot()
    {
        //Debug.Log("spawn");
        Vector3 pos = GetRandomPointOnNavMesh();
        Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot);
        if(bot != null)
        {
            bot.TF.position = pos;
            bot.skinColor.material = ColorManager.Instance.changColor((ColorType)Random.Range(1, 6));
            bot.collider.enabled = true;
            bot.GetTargetIndicator();
            bot.OnInit();
            bots.Add(bot);
            
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

    public PoolType RandomWeapon()
    {
        return (PoolType)Random.Range(2, 6);
    }

    public void changWeaponPlayer(GameObject weapon)
    {
        player.typeWeapon = weapon.GetComponent<Weapon>().weaponType;
        player.ChangeWeaponImg();
    }

    private void InitPointScale()
    {
        basePoints.Add(new BasePoint { Score = 1, Scale = 1f, DeadScore = 1 });
        basePoints.Add(new BasePoint { Score = 2, Scale = 1.2f, DeadScore = 2 });
        basePoints.Add(new BasePoint { Score = 5, Scale = 1.8f, DeadScore = 3 });
        basePoints.Add(new BasePoint { Score = 10, Scale = 2f, DeadScore = 4 });
        basePoints.Add(new BasePoint { Score = 15, Scale = 2.2f, DeadScore = 5 });
        basePoints.Add(new BasePoint { Score = 25, Scale = 2.5f, DeadScore = 7 });
        
    }
    public Weapon GetCurrentWeapon(int index)
    {
        return weapons[index];
    }


}
