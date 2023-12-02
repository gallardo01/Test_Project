using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorController : Singleton<ColorController>
{

    [SerializeField] private Material[] colorMats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Material getColorMaterial(ColorType colorType){
        return colorMats[(int)colorType];
    }
}
