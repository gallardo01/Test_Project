using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlay : MonoBehaviour
{
    [SerializeField] Button Pause;
    private void Start()
    {
        Pause.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.PauseGame));
    }
}
