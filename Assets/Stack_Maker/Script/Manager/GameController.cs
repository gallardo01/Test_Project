using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { MainMenu, GamePlay,Finish, Lose}
public class GameController : Singleton<GameController>
{
    private GameState currentState;
    public int Gold;
    public int Diamond;
    public int totalBrick;
    private void Awake()
    {
        Gold = 0;
        Diamond = 0;
        UIManager.Instance.setGold(Gold);
        UIManager.Instance.setDiamond(Diamond);
    }

    public void collect()
    {
        Debug.Log("up");
        UIManager.Instance.setGold(Gold);
        Diamond += Random.Range(100, 500) ;
        UIManager.Instance.setDiamond(Diamond);
    }


    public void changeState(GameState state)
    {
        currentState = state;
    }

    public bool isState(GameState state)
    {
        return currentState == state;
    }


}
