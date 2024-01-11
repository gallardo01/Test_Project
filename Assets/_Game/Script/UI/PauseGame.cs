using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] Button Home;
    [SerializeField] Button Continue;

    private void Start()
    {
        Home.onClick.AddListener(() => BackMenu());
        Continue.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.GamePlay));
    }
    public void BackMenu()
    {
        SimplePool.CollectAll();
        UIManager.Instance.OpenMainMenu();
    }
}
