using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] private Button retry, next;

    // Start is called before the first frame update
    void Start()
    {
        retry.onClick.AddListener(() => TryAgain());
        next.onClick.AddListener(() => Next());
    }

    void StartGame()
    {
        GameManager.Instance.PlayGame();
        LevelManager.Instance.OnInit();
    }
    void TryAgain()
    {
        PlayerPrefs.SetInt("Start Mode", 1);
        GameManager.Instance.LoadScene();
        // Invoke(nameof(StartGame), 1f);
    }

    public void Next()
    {
        PlayerPrefs.SetInt("Start Mode", 0);
        GameManager.Instance.LoadScene();
    }
}
