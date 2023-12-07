using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class ColorObject : MonoBehaviour
{

    public ColorType colorType;
    [SerializeField] private Renderer rendere;
    [SerializeField] private ColorData colordata;


    public void changColor(ColorType colorType)
    {
        this.colorType = colorType;
        rendere.material = colordata.getMaterial(colorType);
    }
}
