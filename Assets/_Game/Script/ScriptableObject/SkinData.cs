using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkinData", menuName = "ScriptableObjects/SkinData", order = 1)]

public class SkinData : ScriptableObject
{

    public GameObject Prefabs;
    public string Description;
    public int Price;
    public Sprite Image;
    public Material Material;
    public float Speed;
    public float AttackRange;

}
