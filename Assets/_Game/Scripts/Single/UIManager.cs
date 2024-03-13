using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private Player player;

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
    [SerializeField] private RectTransform[] skinPage;
    [SerializeField] private SkinList skinData;
    [SerializeField] private Button closeSkinShopButton;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TextMeshProUGUI skinPrice;
    [SerializeField] private TextMeshProUGUI skinEffect;
    [SerializeField] private Button buySkinButton;
    [SerializeField] private Button equipSkinButton;

    private Image[] backgroundImages;
    private Image[] iconImages;
    private int currentSkinPage;
    private int currentTopButton;

    // Equipped skin normally gets destroyed when equip is called
    // This variable is used to store and destroy the trying equipment, which is not stored in the equipped set
    private SkinItem currentSkinItem;
    private SkinItemData data; // Data of skin currently viewed

    // In Game
    [Header("In Game")]
    [SerializeField] TextMeshProUGUI remainingCount;

    // Other
    [Header("Other")]
    [SerializeField] private JoystickControl joystickControl;
    [SerializeField] private float shiftTime;
    [SerializeField] private Button tryAgainButton;

    private int currentWeapon;
    private Weapon weapon;

    private void Start()
    {
        player = FindObjectOfType<Character>();
        player.InitEquipments();

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
        
        // Before the skin shop is load, default page to -1
        currentSkinPage = -1;

        for (int i = 0; i < skinData.SkinLists.Length; i++)
        {
            for (int j = 0; j < skinData.SkinLists[i].Items.Length; j++)
            {
                int page = i, index = j;

                GameObject skinItemButton = new GameObject("Skin Item Button");
                skinItemButton.transform.SetParent(skinPage[i].transform);
                skinItemButton.AddComponent<Button>().onClick.AddListener(delegate { OnSelectSkin(page, index); });
                skinItemButton.AddComponent<Image>().sprite = skinData.SkinLists[i].Items[j].sprite;
            }

            skinPage[0].gameObject.SetActive(false);
        }

        for (int i = 0; i < topButtons.Count; i++)
        {
            int n = i;
            topButtons[i].onClick.AddListener(delegate { ChangeSkinPanel(n); });
        }

        closeSkinShopButton.onClick.AddListener(OnCloseSkinShop);

        backgroundImages = new Image[topButtons.Count];
        iconImages = new Image[topButtons.Count];

        for (int i = 0; i < topButtons.Count; i++)
        {
            backgroundImages[i] = topButtons[i].GetComponent<Image>();
            iconImages[i] = topButtons[i].transform.GetChild(0).GetComponent<Image>();
        }

        equipSkinButton.onClick.AddListener(OnBuySkin);

        buySkinButton.onClick.AddListener(OnBuySkin);

        skinShop.SetActive(false);

        // Other
        tryAgainButton.onClick.AddListener(OnRetry);

    }

    private void OnRetry()
    {
        LevelManager.Instance.OnRetry();
        tryAgainButton.gameObject.SetActive(false);
    }

    private void OnCloseSkinShop()
    {
        skinShop.SetActive(false);
        UnHideMenuButton();
        LevelManager.Instance.MainCharacter.ChangeAnim(Constants.IDLE_ANIM);
        DisableTopButton();

        currentSkinItem.UnEquip();
        if (player.EquippedSkin[currentSkinPage]) {
            player.EquippedSkin[currentSkinPage].Equip(player, false);
        }
    }

    private void Update()
    {
        if (remainingCount) remainingCount.SetText("Count: " + LevelManager.Instance.remainingPlayerCount);
        if (weaponShop.activeInHierarchy)
        {
            weapon.transform.Rotate(new Vector3(0, weaponRotateSpeed * Time.deltaTime, 0), Space.Self);
        }
    }

    private void OnSelectSkin(int page, int index)
    {
        data = skinData.GetSkin(page, index);

        EquipSkin(data, true);

        // Set UI of buy button
        if (PlayerPrefs.GetInt(data.skinName, 0) == 0) {
            skinPrice.text = data.cost.ToString();
            buySkinButton.gameObject.SetActive(true);
            equipSkinButton.gameObject.SetActive(false);
        }
        else {
            equipSkinButton.gameObject.SetActive(true);
            buySkinButton.gameObject.SetActive(false);
        }
    }

    private void OnBuySkin()
    {
        PlayerPrefs.SetInt(data.skinName, 1);
        buySkinButton.gameObject.SetActive(false);
        equipSkinButton.gameObject.SetActive(true);
        EquipSkin(data, false);
    }

    private void EquipSkin(SkinItemData data, bool trying) {
        if (currentSkinItem) currentSkinItem.UnEquip();

        // Set currentskinitem to refer to the current equipping one
        currentSkinItem = data.skinItem.Equip(player, trying);
    }

    // Load assets using addressable
    private void ChangeSkinPanel(int index)
    {
        if (currentSkinPage != -1) {
            skinPage[currentSkinPage].gameObject.SetActive(false);
            if (player.EquippedSkin[currentSkinPage]) {
                player.EquippedSkin[currentSkinPage].Equip(player, false);
            }
        }

        currentSkinPage = index;

        // Change color of the old top button to negative
        DisableTopButton();
        currentTopButton = index;
        // Change color of the new top button to positive
        ActiveTopButton();

        // Call once to hide the currently equipped skin
        if (player.EquippedSkin[index]) {
            player.EquippedSkin[index].UnEquip();
        }

        skinPage[currentSkinPage].gameObject.SetActive(true);
        scrollRect.content = skinPage[currentSkinPage];
        OnSelectSkin(currentSkinPage, 0);
    }

    private void OnSkinShop()
    {
        HideButton();
        currentTopButton = 0;

        ActiveTopButton();

        skinShop.SetActive(true);

        LevelManager.Instance.MainCharacter.ChangeAnim(Constants.SKIN_DANCE_ANIM);

        ChangeSkinPanel(0);
    }

    private void DisableTopButton()
    {
        backgroundImages[currentTopButton].color = disableBackgroundColor;
        iconImages[currentTopButton].color = disableIconColor;
    }

    private void ActiveTopButton()
    {
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

    public void ShowEndGameUI()
    {
        tryAgainButton.gameObject.SetActive(true);
    }
}
