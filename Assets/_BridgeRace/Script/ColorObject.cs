using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    [SerializeField] private Renderer renderer;
    public ColorType colorType;
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = ColorController.Instance.getColorMaterial(colorType);
    }
}
