using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject Menu;
    public GameObject Play;
    public GameObject NextLevel;
    public GameObject Lose;
    public Text text_gold;
    public Text text_diamond;

    void Start()
    {
        UIManager.Instance.OpenMenu();
    }
    public void OpenMenu()
    {
        Menu.SetActive(true);
        Play.SetActive(false);
        NextLevel.SetActive(false);
        Lose.SetActive(false);
    }
    public void OpenPlay()
    {
        Menu.SetActive(false);
        Play.SetActive(true);
        NextLevel.SetActive(false);
        Lose.SetActive(false );
    }

    public void OpenNextLevel()
    {
        NextLevel.SetActive(true);
        Play.SetActive(false);
        Menu.SetActive(false);
        Lose.SetActive(false);
    }

    public void LoseMenu() {
        Lose.SetActive(true);
    }
    public void PlayButton()
    {
        Play.SetActive(false);
        NextLevel.SetActive(false);
        levelManager.Instance.OnStart();
    }

    public void NextButton()
    {
        Play.SetActive(false);
        NextLevel.SetActive(false);
        levelManager.Instance.NextLevel();
    }
    public void ExitButton()
    {
        OpenMenu();
        levelManager.Instance.level = levelManager.Instance.levels.Count;
        levelManager.Instance.LoadLevel();
        GameController.Instance.changeState(GameState.MainMenu);
    }

    public void setGold(int gold)
    {
        text_gold.text = gold.ToString();
    }
    public void setDiamond(int diamond)
    {
        text_diamond.text = diamond.ToString();
    }

    public void Level1()
    {
        OpenPlay();
        levelManager.Instance.level = 1;
        levelManager.Instance.LoadLevel();
        
    }
    public void Level2()
    {
        OpenPlay();
        levelManager.Instance.level = 2;
        levelManager.Instance.LoadLevel();
        
    }
    public void Level3()
    {
        OpenPlay();
        levelManager.Instance.level = 3;
        levelManager.Instance.LoadLevel();
        
    }

    public void buttonMenu()
    {
        OpenMenu();
        levelManager.Instance.level = levelManager.Instance.levels.Count;
        levelManager.Instance.LoadLevel();
        GameController.Instance.changeState(GameState.MainMenu);
    }
    public void buttonRetry()
    {
        OpenPlay();
        levelManager.Instance.LoadLevel();
    }
    
 
}
