using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterColor : MonoBehaviour
{
    public ColorType colorType;
    public SkinnedMeshRenderer rendere;
    [SerializeField] private ColorData colorData;

    public void changColor(ColorType colorType)
    {
        this.colorType = colorType;
        rendere.material = colorData.getMaterial(colorType);
    }
}
