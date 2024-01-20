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
        next.onClick.AddListener(() => TryAgain());

    }

    void TryAgain()
    {
        GameManager.Instance.TryAgain();
    }

    void Next()
    {
        GameManager.Instance.Next();
    }
}
