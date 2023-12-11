using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton<ColorController>
{

    [SerializeField] Material[] colorMats;

    public Material getColorMaterial(ColorType colorType) { 
        return colorMats[(int) colorType];
    }
}
