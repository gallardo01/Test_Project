using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject Enermy1;

    private void Start()
    {
        Invoke(nameof(Spawn), 10f);
    }
    void Spawn()
    {
        float x = UnityEngine.Random.Range(-96f, 26f);
        Instantiate(Enermy1, new Vector3(x, 6.9f, 0), Quaternion.identity);
        Invoke(nameof(Spawn), 10f);
    }
}
