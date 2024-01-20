using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startGame;
    [SerializeField] private Button changeWeapon;
    [SerializeField] private Button openShop;

    // Start is called before the first frame update
    void Start()
    {
        startGame.onClick.AddListener(() => StartGame());
        changeWeapon.onClick.AddListener(() => ChangeWeapon());
    }

    // Update is called once per frame
    void StartGame()
    {
        GameManager.Instance.PlayGame();
        LevelManager.Instance.OnInit();
    }

    private void ChangeWeapon(){
        GameManager.Instance.ChangeWeapon();
    }
}
