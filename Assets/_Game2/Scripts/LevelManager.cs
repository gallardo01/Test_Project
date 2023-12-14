using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ColorType{
    Default,
    Black,
    Blue,
    Cyan,
    Green,
    Pink
}
public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() {ColorType.Black, ColorType.Blue, ColorType.Cyan, ColorType.Green, ColorType.Pink};
    public Player player;
    public int botAmount;
    public Transform startPoint, finishPoint;
    public Vector3 FinishPoint => finishPoint.position;
    private List<Bot> bots = new List<Bot>();
    [SerializeField] GameObject botPrefab;
    public int CharacterAmount => botAmount +1;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        // position
        List<Vector3> startPoints = new List<Vector3>();
        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(startPoint.position + Vector3.right * 5f * i);
        }

        // player
        List<ColorType> colorDatas = colorTypes;
        int rand = UnityEngine.Random.Range(0, colorDatas.Count);
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        int randPosition = UnityEngine.Random.Range(0, CharacterAmount);
        player.transform.position = startPoints[randPosition];
        startPoints.RemoveAt(randPosition);

        //bot
        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            Bot bot = Instantiate(botPrefab, startPoints[i], Quaternion.identity).GetComponent<Bot>();
            bot.ChangeColor(colorDatas[i]);
            bots.Add(bot);
        }
    }
}
