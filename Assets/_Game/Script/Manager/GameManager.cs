using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay,PauseGame, EndGame, ShopWeapon,ShopSkin, Setting, GiftCode }

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;
    private int coin;
    public int Coin { get => coin; set 
        {
            coin = value;
            SaveManager.Instance.SaveGame();
        }
    }
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
