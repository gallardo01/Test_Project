using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelManager : Singleton<levelManager>
{
    

    public List<Level> levels = new List<Level>();
    public GameObject player;
    //public GameObject playerPref;
    public Level currentLevel;
    public int level = 1;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    public void LoadLevel()
    {
        GameController.Instance.totalBrick = 0;
        LoadLevel(level);
        OnInit();
        
    }
    void OnInit()
    {
        //Debug.Log("oninit");
        player.transform.position = currentLevel.startPoint.position;
        player.gameObject.GetComponent<Player>().OnInit();
        
        
    }
    private void OnDespawn()
    { 
        
    }

    public void LoadLevel(int index)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels[index - 1]);
        //Instantiate(playerPref, Vector3.zero, Quaternion.identity);
        //playerPref.transform.SetParent(currentLevel.transform);
    }

    public void OpenLevel(int levelID)
    {

    }

    public void OnStart()
    {
        //GameController.Instance.changeState(GameState.GamePlay);
    }

    public void NextLevel()
    {
        level++;
        LoadLevel();
        UIManager.Instance.OpenPlay();
    }

    public void OnFinish()
    {
        currentLevel.setWin();
        currentLevel.PlayParticleSystem();
    }

    public void ResetLevel()
    {
        Debug.Log("reset");
        level = 1;
        LoadLevel();
    }
}
