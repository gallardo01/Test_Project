using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public enum GameState { MainMenu, GamePlay,Finish, Lose}
public class GameController : Singleton<GameController>
{
    public Button[] buttonLevel;

    private GameState currentState;
    public int Gold;
    public int Diamond;
    public int totalBrick;
    private void Awake()
    {

        //PlayerPrefs.DeleteAll();

        Diamond = PlayerPrefs.GetInt("Diamond",0);
        UIManager.Instance.setGold(Gold);
        UIManager.Instance.setDiamond(Diamond);
        levelManager.Instance.player.SetActive(false);

        int unlockedLevel =  PlayerPrefs.GetInt("Unlocked" ,1);
        for (int i = 0; i < buttonLevel.Length; i++)
        {
            buttonLevel[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttonLevel[i].interactable = true;
        }
    }

    public void Increase()
    {
        
        UIManager.Instance.setGold(Gold);
        Diamond += Random.Range(100, 500) ;
        PlayerPrefs.SetInt("Diamond", Diamond);
        PlayerPrefs.Save();
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


    public void UnlockLevel()
    {
        PlayerPrefs.SetInt("Unlocked", PlayerPrefs.GetInt("Unlocked", 1) + 1);

        PlayerPrefs.Save();
        int unlockedLevel = PlayerPrefs.GetInt("Unlocked", 1);
        for (int i = 0; i < unlockedLevel; i++)
        {
            buttonLevel[i].interactable = true;
        }
    }

}
