using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.tvOS;

public enum ColorType { Default, Red, Green, Blue, Pink, Yellow };

public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes  = new List<ColorType>() {ColorType.Red, ColorType.Green, ColorType.Blue, ColorType.Pink, ColorType.Yellow };
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        
        List<ColorType> colors = colorTypes;
        int rand  = Random.Range(0,colors.Count);
        player.changColor(colors[rand]);
        colors.RemoveAt(rand);
    }

    
}
