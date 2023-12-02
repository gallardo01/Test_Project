using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorType {
    Default,
    Red,
    Yellow,
    Green,
    Blue,
    Purple
}

public class ColorController : Singleton<ColorController>
{

    [SerializeField] Material[] colorMats;

    public Material getColorMaterial(ColorType colorType) { 
        return colorMats[(int) colorType];
    }
}
