using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // enum
    private enum SkinType
    {
        Hat = 0,
        Pant = 1,
        Shield = 2,
        Set = 3
    }

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

    // Panel
    [Header("Panel")]
    [SerializeField] private GameObject decoration;
    [SerializeField] private GameObject weaponShop;
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
    [SerializeField] private Color backgroundColor;
    [SerializeField] private Color disableColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private RectTransform itemPanel;
    [SerializeField] private GameObject skinItem;
    [SerializeField] private List<Button> topButtons;

    private int currentSkinPage = 0;
    private List<Button>[] buttonGroups;


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

    }

    private void Update()
    {
        if (remainingCount) remainingCount.SetText("Count: " + LevelManager.Instance.remainingBotCount);
        if (weaponShop.activeInHierarchy)
        {
            weapon.transform.Rotate(new Vector3(0, weaponRotateSpeed * Time.deltaTime, 0), Space.Self);
        }
    }

    // Load assets using addressable
    public void ChangeSkinPanel(int index)
    {
        foreach (Button button in buttonGroups[currentSkinPage])
        {
            button.gameObject.SetActive(false);
        }

        if (buttonGroups[index].Count == 0)
        {
            buttonGroups[(int)SkinType.Hat].Add(Instantiate(skinItem, itemPanel).GetComponent<Button>());
        }
        else
        {
            foreach (Button button in buttonGroups[index])
            {
                button.gameObject.SetActive(true);
            }
        }

    }

    private void OnSkinShop()
    {
        HideButton();
        if (buttonGroups[(int)SkinType.Hat].Count == 0)
        {
            buttonGroups[(int)SkinType.Hat].Add(Instantiate(skinItem, itemPanel).GetComponent<Button>());
        }
        currentSkinPage = 0;
    }

    private void CloseWeaponShop()
    {
        weaponShop.SetActive(false);
        UnHideButton();
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

    private void UnHideButton()
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
