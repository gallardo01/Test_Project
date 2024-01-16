using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] Button Play;
    [SerializeField] Button Weapon;
    [SerializeField] Button Skin;
    [SerializeField] Button Setting;
    [SerializeField] TextMeshProUGUI CoinText;
    [SerializeField] RectTransform PosShop;

    private void Start()
    {
        Play.onClick.AddListener(() => OnPlay());
        Weapon.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.ShopWeapon));
        Skin.onClick.AddListener(() => OpenShop());
        
    }
    private void OnEnable()
    {
        CoinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
    private void OnPlay()
    {
        UIManager.Instance.OpenCanvasUI(GameState.GamePlay);
        LevelManager.Instance.OnInit();
    }
    private void OpenShop()
    {
        UIManager.Instance.OpenCanvasUI(GameState.ShopSkin);
        //LevelManager.Instance.player.transform.SetParent(PosShop, false);
    }
}
