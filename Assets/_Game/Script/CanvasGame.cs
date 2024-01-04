using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGame : Singleton<CanvasGame>
{
    public GameObject getCanvas()
    {
        return gameObject;
    }
}
