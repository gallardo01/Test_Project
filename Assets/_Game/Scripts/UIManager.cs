using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // Panel
    [Header("Panel")]
    [SerializeField] private GameObject decoration;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject ingame;

    // Menu
    [Header("Menu")]
    [SerializeField] private Button weaponButton;
    [SerializeField] private Button skinButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Transform coin;
    [SerializeField] private Transform coinShiftPosition;
    [SerializeField] private Transform weaponShiftTransform;
    [SerializeField] private Transform skinShiftTransform;
    [SerializeField] private Transform playShiftTransform;

    // Weapon Shop
    [Header("Weapon Shop")]
    [SerializeField] private GameObject weaponShop;
    [SerializeField] private Button nextItem;
    [SerializeField] private Button previousItem;
    [SerializeField] private Transform itemPlace;
    [SerializeField] private TextMeshProUGUI itemName, itemPrice, itemEffect;
    [SerializeField] private WeaponList weapons;
    [SerializeField] private Button buyWeaponButton;
    [SerializeField] private Button closeWeaponShopButton;
    [SerializeField] private float weaponRotateSpeed;

    private Vector3 originalWeaponButtonPosition, originalSkinButtonPosition, originalPlayButtonPosition;

    // Skin Shop
    [Header("Skin Shop")]
    [SerializeField] private GameObject skinShop;
    [SerializeField] private Color activeBackgroundColor;
    [SerializeField] private Color disableBackgroundColor;
    [SerializeField] private Color activeIconColor;
    [SerializeField] private Color disableIconColor;
    [SerializeField] private List<Button> topButtons;
    [SerializeField] private GameObject[] skinPage;
    [SerializeField] private SkinList skinData;
    [SerializeField] private Button closeSkinShopButton;
    [SerializeField] private TextMeshProUGUI skinPrice;
    [SerializeField] private TextMeshProUGUI skinEffect;
    [SerializeField] private Button buySkinButton;

    private Image[] backgroundImages;
    private Image[] iconImages;
    private int currentSkinPage;
    private int currentTopButton;
    private SkinItem currentSkinItem;

    // In Game
    [Header("In Game")]
    [SerializeField] TextMeshProUGUI remainingCount;

    // Other
    [Header("Other")]
    [SerializeField] JoystickControl joystickControl;
    [SerializeField] float shiftTime;

    private int currentWeapon;
    private Weapon weapon;

    private void Start()
    {
        // Panel
        decoration.SetActive(false);
        ingame.SetActive(false);
        menu.SetActive(true);
        weaponShop.SetActive(false);

        // Weapon Shop
        nextItem.onClick.AddListener(Next);
        previousItem.onClick.AddListener(Previous);
        buyWeaponButton.onClick.AddListener(BuyWeapon);
        closeWeaponShopButton.onClick.AddListener(CloseWeaponShop);

        originalPlayButtonPosition = playButton.transform.position;
        originalWeaponButtonPosition = weaponButton.transform.position;
        originalSkinButtonPosition = skinButton.transform.position;

        // Menu
        playButton.onClick.AddListener(OnPlay);
        playButton.onClick.AddListener(LevelManager.Instance.OnPlay);

        skinButton.onClick.AddListener(OnSkinShop);

        weaponButton.onClick.AddListener(OnWeaponShop);
        weaponButton.onClick.AddListener(OpenWeaponShop);

        // Skin Shop

        for (int i = 0; i < skinData.SkinLists.Length; i++) {
            for (int j = 0; j < skinData.SkinLists[i].Items.Length; j++) {
                int page = i, index = j;

                GameObject skinItemButton = new GameObject("Skin Item Button");
                skinItemButton.transform.SetParent(skinPage[i].transform);
                skinItemButton.AddComponent<Button>().onClick.AddListener(delegate { OnSelectSkin(page, index); } );
                skinItemButton.AddComponent<Image>().sprite = skinData.SkinLists[i].Items[j].sprite;
            }

            skinPage[0].SetActive(false);
        }

        for (int i = 0; i < topButtons.Count; i++)
        {
            int n = i;
            topButtons[i].onClick.AddListener(delegate { ChangeSkinPanel(n); });
        }

        closeSkinShopButton.onClick.AddListener(CloseSkinShop);

        backgroundImages = new Image[topButtons.Count];
        iconImages = new Image[topButtons.Count];

        for (int i = 0; i < topButtons.Count; i++) {
            backgroundImages[i] = topButtons[i].GetComponent<Image>();
            iconImages[i] = topButtons[i].transform.GetChild(0).GetComponent<Image>();
        }

        buySkinButton.onClick.AddListener(GetSkin);

        skinShop.SetActive(false);
    }

    private void CloseSkinShop() {
        skinShop.SetActive(false);
    }

    private void Update()
    {
        if (remainingCount) remainingCount.SetText("Count: " + LevelManager.Instance.remainingBotCount);
        if (weaponShop.activeInHierarchy)
        {
            weapon.transform.Rotate(new Vector3(0, weaponRotateSpeed * Time.deltaTime, 0), Space.Self);
        }
    }

    private void OnSelectSkin(int page, int index) {
        currentSkinItem = skinData.GetSkin(page, index).prefab;
    }

    private void GetSkin()
    {
        currentSkinItem.Equip();
    }

    // Load assets using addressable
    private void ChangeSkinPanel(int index)
    {
        skinPage[currentSkinPage].SetActive(false);

        currentSkinPage = index;

        DisableTopButton();
        currentTopButton = index;
        ActiveTopButton();

        skinPage[currentSkinPage].SetActive(true);
    }

    private void OnSkinShop()
    {
        HideButton();
        currentTopButton = 0;

        ActiveTopButton();

        skinShop.SetActive(true);
        currentSkinPage = 0;
        skinPage[currentSkinPage].SetActive(true);
    }

    private void DisableTopButton() {
        backgroundImages[currentTopButton].color = disableBackgroundColor;
        iconImages[currentTopButton].color = disableIconColor;
    }

    private void ActiveTopButton() {
        backgroundImages[currentTopButton].color = activeBackgroundColor;
        iconImages[currentTopButton].color = activeIconColor;
    }

    private void CloseWeaponShop()
    {
        weaponShop.SetActive(false);
        UnHideMenuButton();
    }

    private void BuyWeapon()
    {
        LevelManager.Instance.MainCharacter.ChangeWeapon(currentWeapon);
    }

    private void OnPlay()
    {
        joystickControl.enabled = true;
        decoration.SetActive(true);
        ingame.SetActive(true);
        HideButton();
        coin.DOMove(coinShiftPosition.position, shiftTime);
    }

    private void OnWeaponShop()
    {
        // Should be the first weapon not owned
        currentWeapon = 0;
        Next();
        LevelManager.Instance.MainCharacter.gameObject.SetActive(false);
        HideButton();
    }

    private void HideButton()
    {
        weaponButton.transform.DOMove(weaponShiftTransform.position, shiftTime);
        skinButton.transform.DOMove(skinShiftTransform.position, shiftTime);
        playButton.transform.DOMove(playShiftTransform.position, shiftTime);
    }

    private void UnHideMenuButton()
    {
        weaponButton.transform.DOMove(originalWeaponButtonPosition, shiftTime);
        skinButton.transform.DOMove(originalSkinButtonPosition, shiftTime);
        playButton.transform.DOMove(originalPlayButtonPosition, shiftTime);
        LevelManager.Instance.MainCharacter.gameObject.SetActive(true);
    }

    private void Next()
    {
        currentWeapon = currentWeapon == weapons.Size - 1 ? currentWeapon : currentWeapon + 1;
        CreateWeapon();
    }

    private void Previous()
    {
        currentWeapon = currentWeapon == 0 ? currentWeapon : currentWeapon - 1;
        CreateWeapon();
    }

    private void CreateWeapon()
    {
        itemName.SetText(weapons.GetWeapon(currentWeapon).WeaponName);
        if (weapon) Destroy(weapon.gameObject);
        weapon = Instantiate(weapons.GetWeapon(currentWeapon), itemPlace);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        itemPrice.SetText(weapon.Price.ToString());
    }

    private void OpenWeaponShop()
    {
        weaponShop.SetActive(true);
    }
}
