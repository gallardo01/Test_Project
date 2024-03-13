using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pools : MonoBehaviour
{
    [SerializeField] private GameObject bot;
    [SerializeField] private GameObject spray;
    [SerializeField] private ScriptableObject bulletList;

    public static Pool<Bot> botPool;
    public static Pool<ParticleSystem> sprayPool;

    private void Awake()
    {
        // Create spray pool
        sprayPool = new Pool<ParticleSystem>(spray);

        // Create bot pool
        botPool = new Pool<Bot>(bot);
    }
}
