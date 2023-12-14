using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton1<LevelManager>
{
    
    // Start is called before the first frame update
    readonly List<ColorType> colorTypes = new List<ColorType>() {ColorType.Black, ColorType.Red, ColorType.Green, ColorType.Yellow, ColorType.Orange, ColorType.Brown, ColorType.Violet};
    public Player player;
    public int botAmount;
    public int CharacterAmount => botAmount + 1;
    List<Bot> bots = new List<Bot>();
    [SerializeField] GameObject botPrefaps;
    public Transform startPoint;
    public Transform finishPoint;
    public NavMeshData navMeshData;
    public Vector3 FinishPoint => finishPoint.position;
    void Start()
    {
        OnInit();
    }
    void OnInit()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        List<Vector3> startPoints = new List<Vector3>();
        for(int i=0;i<CharacterAmount;i++)
        {
            startPoints.Add(startPoint.position+Vector3.right*2*i);
        }
        List<ColorType> colorDatas = colorTypes;
        int rand = Random.Range(0, colorTypes.Count);
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);
        int randPosition = Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[randPosition];
        startPoints.RemoveAt(randPosition);
        for(int i=0;i<CharacterAmount-1;i++)
        {
            Bot bot = Instantiate(botPrefaps, startPoints[i],Quaternion.identity).GetComponent<Bot>();
            bot.ChangeColor(colorDatas[i]);
            bot.ChangeState(new PatrolState());
            bots.Add(bot);
        }
    }
    
}
