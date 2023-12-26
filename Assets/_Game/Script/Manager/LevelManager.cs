using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{

    public Player player;
    public int botAmount;
    private List<Bot> bots = new List<Bot>();
    private int characterAmount => botAmount + 1;
    [SerializeField] private GameObject BotPref;
    public NavMeshData navMeshData;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        player.skinColor.material = ColorManager.Instance.changColor((ColorType)Random.Range(1, 6));
        // bot
        for (int i = 0; i < botAmount; i++)
        {
            Bot bot = Instantiate(BotPref, GetRandomPointOnNavMesh() , Quaternion.identity).GetComponent<Bot>();
            bot.changState(new PatrolState());
            bot.skinColor.material = ColorManager.Instance.changColor((ColorType)Random.Range(1, 6));

        }
    }
    public Vector3 GetRandomPointOnNavMesh()
    {
        Vector3 randomPoint = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 10f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return randomPoint;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
