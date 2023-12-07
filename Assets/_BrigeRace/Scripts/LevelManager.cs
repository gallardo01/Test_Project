using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
   
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        List<ColorType> colorDatas = colorTypes;
        int rand = Random.Range(0, colorDatas.Count);
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);
        
        // bot

    }

}
