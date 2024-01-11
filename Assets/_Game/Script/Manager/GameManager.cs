using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MainMenu, GamePlay,PauseGame, Finish, ShopWeapon,ShopSkin, Setting }

public class GameManager : Singleton<GameManager>
{
    private static GameState gameState;
    private void Awake()
    {
        gameState = GameState.MainMenu;
    }

    public static void ChangeState(GameState state)
    {
        gameState = state;
    }

    public static bool IsState(GameState state) => gameState == state;
}
