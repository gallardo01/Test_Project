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

    public Vector3 FinishPoint => finishPoint.position;
    private List<Bot> bots = new List<Bot>();
    [SerializeField] private GameObject botPrefabs;

    public int characterAmount => botAmount + 1;

    public GameObject canvasJoystick;
    public bool isWin = false;
   
    // Start is called before the first frame update
    void Awake()
    {
        OnInit();
    }

    public void OnInit()
    {
        NavMesh.RemoveAllNavMeshData();
        NavMesh.AddNavMeshData(navMeshData);
        List<Vector3> startPoints = new List<Vector3>();
        for(int i = 0; i < characterAmount; i++){
            startPoints.Add(startPoint.position + Vector3.right * 3f * i);
        }


        List<ColorType> colorDatas = colorTypes;
        int rand = Random.Range(0, colorDatas.Count);
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        int randPosition = Random.Range(0, characterAmount);
        player.transform.position = startPoints[randPosition];
        startPoints.RemoveAt(randPosition);
        
        // bot
        for(int i = 0; i < characterAmount - 1; i++){
            Bot bot = Instantiate(botPrefabs, startPoints[i], Quaternion.identity).GetComponent<Bot>();
            rand = Random.Range(0, colorDatas.Count);
            Debug.Log("Bot: " + colorDatas[rand]);
            bot.ChangeColor(colorDatas[rand]);
            // bot.GetComponent<characterAmount>
            bot.ChangeState(new PatrolState());
            colorDatas.RemoveAt(rand);
            bots.Add(bot);
        }
    }

    public void WinGame(){
        isWin = true;
        canvasJoystick.SetActive(false);
    }

}
