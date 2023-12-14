using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class BrickSpawner : Singleton<BrickSpawner>
{

    [SerializeField] private Stage stageOne;

    public Stage StageOne => stageOne;

    // Start is called before the first frame update
    void Start()
    {
        foreach (ColorType colorType in GameManager.Ins.UsedColors) {
            stageOne.StartLevel(colorType);
        }
    }

}
