using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.tvOS;

public enum ColorType { Default, Red, Green, Blue, Pink, Yellow };

public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes  = new List<ColorType>() {ColorType.Default, ColorType.Red, ColorType.Green, ColorType.Blue, ColorType.Pink, ColorType.Yellow };
    public Player player;
    public int botAmount;
    public Transform startPoint;
    public Transform finishPoint;
     
    public Vector3 FinishPoint => finishPoint.position;
    public int characterAmount => botAmount+1;

    private List<Bot> listBot = new List<Bot>();
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
        List<ColorType> colors = colorTypes; // color
        List<Vector3> startPoints = new List<Vector3>(); // position
        for (int i = 0; i < characterAmount; i++)
        {

            startPoints.Add(startPoint.position + Vector3.right * 3f * i);
        }
        // player
        int rand  = Random.Range(1,colors.Count);
        player.changColor(colors[rand]);
        colors.RemoveAt(rand);
        int randPos = Random.Range(0, characterAmount);
        player.transform.position = startPoints[randPos];
        startPoints.RemoveAt(randPos);

        // bot
        for (int i = 0; i < botAmount; i++)
        {
            Bot bot = Instantiate(BotPref, startPoints[i], Quaternion.identity).GetComponent<Bot>();
            bot.changColor(colors[i+1]);
            bot.changState(new PatrolState());
            listBot.Add(bot);
        }

    }



    
}
