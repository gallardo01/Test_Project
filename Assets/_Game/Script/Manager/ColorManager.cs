using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
public enum ColorType { Black, Red, Green, Blue, Pink, Yellow };
public class ColorManager : Singleton<ColorManager>
{
    [SerializeField] private ColorData colordata;


    public Material changColor(ColorType colorType)
    {
        return colordata.getMaterial(colorType);
    }
}
