using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject Play;
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject ShopWeapon;
    [SerializeField] GameObject ShopSkin;
    [SerializeField] GameObject Setting;
    [SerializeField] GameObject PauseGame;
    [SerializeField] GameObject EndGame;
    [SerializeField] GameObject Coin;
    [SerializeField] GameObject GiftCode;
    [SerializeField] EndGame EndGameState;
    [SerializeField] TextMeshProUGUI text_Coin;



    public Dictionary<GameState, GameObject> dictStateGameObject = new Dictionary<GameState, GameObject>();
    private void Awake()
    {
        this.AddStates();
        this.RegisterListener(EventID.Win, (param) => ChangeStateEndGame());
        this.RegisterListener(EventID.Lose, (param) => ChangeStateEndGame());
        EndGameState.RegisterListener();
        this.OpenMainMenu();
        TurnOnCoinCanvas();
    }

    public void ChangeStateEndGame()
    {
        this.OpenCanvasUI(GameState.EndGame);
    }

    public void TurnOnCoinCanvas()
    {
        Coin.SetActive(true);
        UpDateCoinText();
    }
    public void TurnOffCoinCanvas()
    {
        Coin.SetActive(false);
    }
    public void UpDateCoinText()
    {
        text_Coin.text = GameManager.Instance.Coin.ToString();
    }
    public void OpenCanvasUI(GameState nextGameState)
    {
        if (GameManager.IsState(nextGameState)) return;
        GameManager.ChangeState(nextGameState);
        foreach (var state in dictStateGameObject)
        {
            if (GameManager.IsState(state.Key))
            {
                state.Value.SetActive(true);
            }
            else
            {
                state.Value.SetActive(false);
            }
        }
    }
    public void OpenMainMenu()
    {

        GameManager.ChangeState(GameState.MainMenu);
        foreach (var state in dictStateGameObject)
        {
            if (GameManager.IsState(state.Key))
            {
                state.Value.SetActive(true);
            }
            else
            {
                state.Value.SetActive(false);
            }
        }
    }

    public void UpdateCoin()
    {

    }
    private void AddStates()
    {
        dictStateGameObject.Add(GameState.MainMenu, MainMenu);
        dictStateGameObject.Add(GameState.GamePlay, Play);
        dictStateGameObject.Add(GameState.ShopWeapon, ShopWeapon);
        dictStateGameObject.Add(GameState.ShopSkin, ShopSkin);
        dictStateGameObject.Add(GameState.PauseGame, PauseGame);
        dictStateGameObject.Add(GameState.EndGame, EndGame);
        dictStateGameObject.Add(GameState.Setting, Setting);
        dictStateGameObject.Add(GameState.GiftCode, GiftCode);

    }
}

