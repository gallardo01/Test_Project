using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHint : MonoBehaviour
{
    private List<Bot> bots;

    private void Start() {
        bots = LevelManager.Instance.Bots;
    }
}
