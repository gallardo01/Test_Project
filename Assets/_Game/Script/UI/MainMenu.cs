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
    [SerializeField] Button Setting;
    [SerializeField] Button Volume;
    [SerializeField] RectTransform PosShop;
    [SerializeField] Image Sound;
    [SerializeField] Image Mute;


    private void Start()
    {
        Play.onClick.AddListener(() => OnPlay());
        Weapon.onClick.AddListener(() => OpenShopWeapon());
        Skin.onClick.AddListener(() => OpenShopSkin());
        Volume.onClick.AddListener(() => ChangVolume());
        
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
    public override void ChangVolume()
    {
        base.ChangVolume();
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
