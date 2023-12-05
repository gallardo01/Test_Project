using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : Singleton1<ColorController>
{
    // Start is called before the first frame update
    [SerializeField] Material[] colorMats;
    public Material getColorMaterial(ColorType colorType)
    {
        return colorMats[(int)colorType];
    }
}
