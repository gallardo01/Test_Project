using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject Play;
    [SerializeField] GameObject MainMenu; 
    [SerializeField] GameObject ShopWeapon;
    [SerializeField] GameObject ShopSkin;
    [SerializeField] GameObject Setting;
    [SerializeField] GameObject PauseGame;
    [SerializeField] GameObject EndGame;
    [SerializeField] EndGame EndGameState;

    public Dictionary<GameState, GameObject> dictStateGameObject = new Dictionary<GameState, GameObject>();

    [SerializeField] GameObject[] weapons;
    private int weapon_index = 0;
    public int total_weapon => weapons.Length;
    private void Awake()
    {
        this.AddStates();
        this.OpenMainMenu();
        this.RegisterListener(EventID.Win, (param) => ChangeStateEndGame());
        this.RegisterListener(EventID.Lose, (param) => ChangeStateEndGame());
        EndGameState.RegisterListener();

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
    public void ChangeStateEndGame()
    {
        this.OpenCanvasUI(GameState.EndGame);
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

    public void UpdateCoin()
    {

    }
    private void AddStates()
    {
        dictStateGameObject.Add(GameState.MainMenu, MainMenu);
        dictStateGameObject.Add(GameState.GamePlay, Play);
        dictStateGameObject.Add(GameState.ShopWeapon, ShopWeapon);
        dictStateGameObject.Add(GameState.ShopSkin, ShopSkin);
        dictStateGameObject.Add(GameState.PauseGame, PauseGame);
        dictStateGameObject.Add(GameState.EndGame, EndGame);
        dictStateGameObject.Add(GameState.Setting, Setting);

    }
    public void FillCanvas()
    {
        MainMenu = GameObject.Find("MainMenu");
        Play = GameObject.Find("Play");
        ShopWeapon = GameObject.Find("ShopWeapon");
        ShopSkin = GameObject.Find("ShopSkin");
        Setting = GameObject.Find("Setting");
        PauseGame = GameObject.Find("PauseGame");
        EndGame = GameObject.Find("EndGame");
    }


}
#if UNITY_EDITOR
[CustomEditor(typeof(UIManager))]
public class UIManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UIManager fillCanvas = (UIManager)target;

        if (GUILayout.Button("Fill Canvas"))
        {
            fillCanvas.FillCanvas();
        }
    }
}
#endif

