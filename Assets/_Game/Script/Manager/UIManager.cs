using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject Play;
    [SerializeField] GameObject MainMenu;



    private void Start()
    {
        Play.SetActive(false);
    }

    public void OnPlay()
    {
        Play.SetActive(true);
        MainMenu.SetActive(false);
        GameManager.ChangeState(GameState.GamePlay);
        LevelManager.Instance.OnInit();
    }
}
