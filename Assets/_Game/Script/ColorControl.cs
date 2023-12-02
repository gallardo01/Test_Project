using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;


public class ColorControl : MonoBehaviour
{

    public ColorType ColorType;
    [SerializeField] private MeshRenderer rendere;
    [SerializeField] private ColorData colordata;


    public void changColor(ColorType colorType)
    {
        this.ColorType = colorType;
        rendere.material = colordata.getMaterial(colorType);
    }
}
