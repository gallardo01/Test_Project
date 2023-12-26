using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject child;


    public void OnEnable()
    {
        child.SetActive(true);
    }

    public void OnDisable()
    {
        child.SetActive(false); 
    }
}
