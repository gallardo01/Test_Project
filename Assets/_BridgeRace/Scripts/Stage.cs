using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    private ColorType[] usedColors;

    public ColorType[] UsedColors { get => usedColors; }
    public static Stage Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        usedColors = new ColorType[4];
        usedColors[0] = ColorType.Default;
        usedColors[1] = ColorType.Red;
        usedColors[2] = (ColorType)Random.Range(2, 4);
        usedColors[3] = (ColorType)Random.Range(4, 6);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
