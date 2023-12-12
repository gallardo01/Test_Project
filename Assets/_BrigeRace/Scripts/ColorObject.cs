using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
    public ColorType colorType;
    [SerializeField] private Renderer objRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        objRenderer.material = ColorController.Ins.getColorMaterial(colorType);
    }
}
