using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GameObject Menu;
    public GameObject Stage;
    public GameObject Play;
    public GameObject NextLevel;
    public GameObject Lose;
    public GameObject Item;
    public GameObject Treasure;
    public Text text_gold;
    public Text text_diamond;

    void Start()
    {
        UIManager.Instance.OpenMenu();
    }

    
    //**********************MENU***************************
    public void OpenMenu()
    {
        GameController.Instance.changeState(GameState.MainMenu);
        Menu.SetActive(true);
        Stage.SetActive(false);
        Play.SetActive(false);
        NextLevel.SetActive(false);
        Lose.SetActive(false);
    }

    public void StageButton()
    {
        OpenStage();
    }

    //**********************STAGE***************************

    public void OpenStage()
    {
        Menu.SetActive(false);
        Stage.SetActive(true);
        Play.SetActive(false);
        NextLevel.SetActive(false);
        Lose.SetActive(false);
        
    }

    public void OpenLevel(int index)
    {
        OpenPlay();
        levelManager.Instance.player.SetActive(true);
        levelManager.Instance.level = index;
        levelManager.Instance.LoadLevel();
    }


    //**********************PLAY***************************

    public void OpenPlay()
    {
        Menu.SetActive(false);
        Stage.SetActive(false);
        Play.SetActive(true);
        NextLevel.SetActive(false);
        Lose.SetActive(false );
    }
    public void PlayButton()
    {
        Play.SetActive(false);
        NextLevel.SetActive(false);
        GameController.Instance.changeState(GameState.GamePlay);
    }


    //**********************NEXT***************************

    public void OpenNextLevel()
    {
        Menu.SetActive(false);
        NextLevel.SetActive(true);
        Play.SetActive(false);
        Stage.SetActive(false);
        Lose.SetActive(false);
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
        levelManager.Instance.player.SetActive(false);
        levelManager.Instance.level = levelManager.Instance.levels.Count;
        levelManager.Instance.LoadLevel();
        GameController.Instance.changeState(GameState.MainMenu);
    }

    //**********************LOSE***************************

    public void LoseMenu() 
    {
        Lose.SetActive(true);
    }

    public void buttonMenu()
    {
        OpenMenu();
        levelManager.Instance.player.SetActive(false);
        levelManager.Instance.level = levelManager.Instance.levels.Count;
        levelManager.Instance.LoadLevel();
        GameController.Instance.changeState(GameState.MainMenu);
    }
    public void buttonRetry()
    {
        OpenPlay();
        levelManager.Instance.LoadLevel();
    }

    //**********************LOSE***************************
    public void OpenTreasure()
    {
        Treasure.SetActive(true);
    }

    public void TreasureButton()
    {
        if(PlayerPrefs.GetInt("Unlocked") == GameController.Instance.buttonLevel.Length && levelManager.Instance.level == GameController.Instance.buttonLevel.Length)
        {
            OpenMenu();
            buttonMenu();
        }
        else
        {
            OpenNextLevel();
        }
        
        GameController.Instance.Increase();
        Treasure.SetActive(false );
    }
    public void OpenIteam()
    {
        
    }
    //************UI******************

    public void setGold(int gold)
    {
        text_gold.text = gold.ToString();
    }
    public void setDiamond(int diamond)
    {
        text_diamond.text = diamond.ToString();
    }




    
 
}
