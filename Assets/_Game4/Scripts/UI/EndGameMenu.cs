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

    void TryAgain()
    {
        GameManager.Instance.LoadScene();
        GameManager.Instance.PlayGame();
        LevelManager.Instance.OnInit();
    }

    void Next()
    {
        GameManager.Instance.LoadScene();
    }
}
