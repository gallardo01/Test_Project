using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class ColorObject : MonoBehaviour
{
    public ColorType colorType;
    [SerializeField] private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = ColorController.Ins.getColorMaterial(colorType);
    }
}
