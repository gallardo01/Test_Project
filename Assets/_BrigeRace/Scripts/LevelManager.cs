using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ColorType
{
    Default,
    Black,
    Red,
    Blue,
    Green,
    Yellow,
    Orange,
    Brown,
    Violet
}
public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() { ColorType.Black, ColorType.Red, ColorType.Blue, ColorType.Green,
    ColorType.Yellow, ColorType.Orange, ColorType.Brown, ColorType.Violet};

    public PlayerController player;

    public int botAmount;
    public Transform startPoint;
    public Transform finishPoint;

    public NavMeshData navMeshData;
    public Stage stage;
    public Vector3 FinishPoint => finishPoint.position;

    private List<Bot> bots = new List<Bot>();
    [SerializeField] GameObject botPrefabs;
    public int CharacterAmount => botAmount + 1;
   
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        // position
        List<Vector3> startPoints = new List<Vector3>();
        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(startPoint.position + Vector3.right * 3f * i);
        }
        // Player
        List<ColorType> colorDatas = colorTypes;
        int rand = Random.Range(0, colorDatas.Count);
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        int randPosition = Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[randPosition];
        startPoints.RemoveAt(randPosition);
        // bot
        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            Bot bot = Instantiate(botPrefabs, startPoints[i], Quaternion.identity).GetComponent<Bot>();
            bot.ChangeColor(colorDatas[i]);
            bot.GetComponent<Character>().stage = stage;
            bot.ChangeState(new PatrolState());
            bots.Add(bot);
        }
    }

}
