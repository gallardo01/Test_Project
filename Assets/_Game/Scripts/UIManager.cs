using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI remainingCount;

    private void Update() {
        remainingCount.SetText("Count: " + LevelManager.Instance.remainingBotCount);
    }

}
