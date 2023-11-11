using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(OnDespawn), 1f);
    }

    private void OnDespawn()
    {
        Destroy(gameObject);
    }
}

