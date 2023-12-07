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
public class LevelManager : MonoBehaviour
{
    readonly List<ColorType> colorTypes = new List<ColorType>() {ColorType.Black, ColorType.Blue, ColorType.Cyan, ColorType.Green, ColorType.Pink};
    public Player player;
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

        //bot
    }
}
