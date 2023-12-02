using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class Brick : ColorControl
{
    private void Start()
    {
        changColor((ColorType)Random.Range(1,6));
    }
}
