using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ColorObject
{
    protected static List<ColorType> usedColors;
    
    // Start is called before the first frame update
    protected void Init()
    {
        if (usedColors == null) usedColors = new List<ColorType>(Stage.Instance.UsedColors);
        int color = Random.Range(0, usedColors.Count);
        usedColors.RemoveAt(color);
        ChangeColor((ColorType) color);
    }
}
