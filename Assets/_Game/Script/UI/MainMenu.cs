using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Button Play;
    [SerializeField] Button Weapon;
    [SerializeField] Button Skin;
    [SerializeField] Button Setting;

    private void Start()
    {
        Play.onClick.AddListener(() => OnPlay());
        Weapon.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.ShopWeapon));
        
    }
    
    private void OnPlay()
    {
        UIManager.Instance.OpenCanvasUI(GameState.GamePlay);
        LevelManager.Instance.OnInit();
    }
}
