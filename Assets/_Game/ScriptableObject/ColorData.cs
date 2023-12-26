using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "ScriptableObjects/ColorData", order = 1)]

public class ColorData : ScriptableObject
{
    public Material[] colorMats;

    public Material getMaterial(ColorType colorType)
    {
        return colorMats[(int)colorType];
    }
}
