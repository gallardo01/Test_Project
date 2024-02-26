using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : CanvasAbs
{

    [SerializeField] Button Play;
    [SerializeField] Button Weapon;
    [SerializeField] Button Skin;
    [SerializeField] Button Code;
    [SerializeField] Button Volume;
    [SerializeField] RectTransform PosShop;
    [SerializeField] Image Sound;
    [SerializeField] Image Mute;


    private void Start()
    {
        Play.onClick.AddListener(() => OnPlay());
        Weapon.onClick.AddListener(() => OpenShopWeapon());
        Skin.onClick.AddListener(() => OpenShopSkin());
        Volume.onClick.AddListener(() => ChangeVolume());
        Code.onClick.AddListener(() => OpenGiftCode());
        
    }
    private void OnEnable()
    {
        ChangeImage();
    }
    private void OnPlay()
    {
        UIManager.Instance.TurnOffCoinCanvas();
        UIManager.Instance.OpenCanvasUI(GameState.GamePlay);
        LevelManager.Instance.OnInit();
        LevelManager.Instance.OnCircleAttack();
    }
    private void OpenShopWeapon()
    {
        UIManager.Instance.TurnOnCoinCanvas();
        UIManager.Instance.OpenCanvasUI(GameState.ShopWeapon);
    }
    private void OpenShopSkin()
    {
        UIManager.Instance.TurnOnCoinCanvas();
        UIManager.Instance.OpenCanvasUI(GameState.ShopSkin);
    }

    private void OpenGiftCode()
    {
        UIManager.Instance.TurnOffCoinCanvas();
        UIManager.Instance.OpenCanvasUI(GameState.GiftCode);
    }
    public override void ChangeVolume()
    {
        base.ChangeVolume();
        ChangeImage();
    }
    private void ChangeImage()
    {
        if (SoundManager.Instance.IsMuted())
        {
            Sound.gameObject.SetActive(false);
            Mute.gameObject.SetActive(true);
        }
        else
        {
            Sound.gameObject.SetActive(true);
            Mute.gameObject.SetActive(false);
        }
    }

}
