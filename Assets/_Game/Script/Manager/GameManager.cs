using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay,PauseGame, EndGame, ShopWeapon,ShopSkin, Setting }

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;
    public int Coin;
    private void Awake()
    {
        gameState = GameState.MainMenu;
        if (!PlayerPrefs.HasKey("Coin"))
        {
            PlayerPrefs.SetInt("Coin", 0);
        }
        else
        {
            Coin = PlayerPrefs.GetInt("Coin");
        }
        //UpdateCoin(4000);
    }

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state;

    public void UpdateCoin(int coin)
    {
        this.Coin += coin;   
    }
}
