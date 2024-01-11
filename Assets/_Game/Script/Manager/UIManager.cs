using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject Play;
    [SerializeField] GameObject MainMenu; 
    [SerializeField] GameObject ShopWeapon;
    [SerializeField] GameObject ShopSkin;
    [SerializeField] GameObject Setting;
    [SerializeField] GameObject PauseGame;

    public Dictionary<GameState, GameObject> dictStateGameObject = new Dictionary<GameState, GameObject>();

    [SerializeField] GameObject[] weapons;
    private int weapon_index = 0;
    public int total_weapon => weapons.Length;
    private void Awake()
    {
        this.AddStates();
        this.OpenMainMenu();
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("Weapon"))
        {
            PlayerPrefs.SetInt("Weapom", 0);
        }
        else
        {
            weapon_index = PlayerPrefs.GetInt("Weapon");
        }
       
    }

    public GameObject GetCurrentWeapon(int index)
    {
        return weapons[index];   
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
    private void AddStates()
    {
        dictStateGameObject.Add(GameState.MainMenu, MainMenu);
        dictStateGameObject.Add(GameState.GamePlay, Play);
        dictStateGameObject.Add(GameState.ShopWeapon, ShopWeapon);
        dictStateGameObject.Add(GameState.ShopSkin, ShopSkin);
        dictStateGameObject.Add(GameState.PauseGame, PauseGame);

    }



}
