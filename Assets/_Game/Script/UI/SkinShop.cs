using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public enum ShopState
{
    Hat = 0,
    Pant = 1,
    Shield = 2,
    FullSet = 3,
}


public class SkinShop : CanvasAbs
{
    private Player player;

    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Button Exit;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] Button Buy;
    [SerializeField] TextMeshProUGUI Price;
    [SerializeField] GameObject FrameChooseItem;

    [SerializeField] List<Button> buttons;
    [SerializeField] List<GameObject> views; 
    public Transform contentHat;
    public Transform contentPan;
    public Transform contentShield;
    public Transform contentFullSet;

    public RectTransform firstHat;
    public RectTransform firstPant;

    public UnityAction<int> onButtonClicked;
    public GameObject currentView;
    public Button currentButton;
    public int currentIndex;
    public ShopState shopState;

    public GameObject currentHat;
    public Material currentPant;
    public RectTransform PosShop;
    private void Awake()
    {
        this.player = LevelManager.Instance.player;
        LoadShopHat();
        LoadShopPant();
        Exit.onClick.AddListener(() => BackToMainMenu());
        Buy.onClick.AddListener(() => BuyOrSelectItem());
    }
    void Start()
    { 
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i;
            buttons[i].GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
        onButtonClicked += ChangeView;

    }

    private void OnEnable()
    {
        this.player.ChangAnim(Constants.ANIM_DANCE);
        ChangeView(0);
    }
    public override void BackToMainMenu()
    {
        base.BackToMainMenu();
        ClearChoices();
        this.player.ChangAnim(Constants.ANIM_IDLE);
    }
    private void ClearChoices()
    {
        player.PanType.material = player.pantCurrent;
        if (player.hatCurrent != null) player.hatCurrent.SetActive(true);
        Destroy(this.currentHat);

    }
    void OnButtonClick(int buttonIndex)
    {
        onButtonClicked?.Invoke(buttonIndex);
    }

    void ChangeView(int index)
    {
        if(currentButton != null)
        {
            currentButton.image.enabled = false;
            currentView.gameObject.SetActive(false);
        }
        if(index < buttons.Count)
        {
            currentButton = buttons[index];
            currentButton.image.enabled = true;
            currentView = views[index];
            currentView.gameObject.SetActive(true);
            shopState = (ShopState)index;
            ClearOldItem(index);
            ChangFirstItem(index);
        }
    }

    private void ClearOldItem(int indexView)
    {
        if (player.pantCurrent != player.PanType.material) player.PanType.material = player.pantCurrent;
        if (indexView != 0)
        {
            if (player.hatCurrent != null) player.hatCurrent.SetActive(true);
            Destroy(this.currentHat);
        }
        if (indexView != 1) player.PanType.material = player.pantCurrent;

    }
    private void ChangFirstItem(int index)
    {
        switch(index)
        {
            case 0:
                ChangeHat(0, firstHat); break;
            case 1: 
                ChangePant(0, firstPant); break;

            default: break;
        }
    }
    private void LoadShopHat()
    {
        int count = DataManager.Instance.hatDatas.Count;
        GameObject firstButton = Instantiate(ButtonPrefab, contentHat);
        firstButton.GetComponent<Image>().sprite = DataManager.Instance.hatDatas[0].Image;
        firstButton.GetComponent<ButtonCharSkin>().index = 0;
        firstButton.GetComponent<ButtonCharSkin>().action += ChangeHat;
        firstHat =(RectTransform)firstButton.gameObject.transform;
        for (int i = 1; i < count; i++)
        {
            GameObject myButton = Instantiate(ButtonPrefab, contentHat);
            myButton.GetComponent<Image>().sprite = DataManager.Instance.hatDatas[i].Image;
            myButton.GetComponent<ButtonCharSkin>().index = i;
            myButton.GetComponent<ButtonCharSkin>().action += ChangeHat;           
        }

    }


    // Mac thu
    private void ChangeHat(int index, RectTransform pos)
    {
        if(player.hatCurrent != null)
        {
            player.hatCurrent.SetActive(false);
        }
        if(currentHat != null) { Destroy(currentHat); } 
        currentIndex = index;
        currentHat = Instantiate(DataManager.Instance.hatDatas[index].Prefabs, player.HatPoint);
        Description.text = DataManager.Instance.hatDatas[index].Description;
        FrameChooseItem.transform.SetParent(pos, false);
        if (SaveManager.Instance.listBoughtHatID.Contains(index))
        {
            ChangeStateSelect();
            return;
        }
        else
        {
            Price.text = DataManager.Instance.hatDatas[index].Price.ToString();
        }
        


    }
    private void LoadShopPant()
    {
        int count = DataManager.Instance.panDatas.Count;
        GameObject firstButton = Instantiate(ButtonPrefab, contentPan);
        firstButton.GetComponent<Image>().sprite = DataManager.Instance.panDatas[0].Image;
        firstButton.GetComponent<ButtonCharSkin>().index = 0;
        firstButton.GetComponent<ButtonCharSkin>().action += ChangePant;
        firstPant = (RectTransform)firstButton.gameObject.transform;
        for (int i = 1; i < count; i++)
        {
            GameObject myButton = Instantiate(ButtonPrefab, contentPan);
            myButton.GetComponent<Image>().sprite = DataManager.Instance.panDatas[i].Image;
            myButton.GetComponent<ButtonCharSkin>().index = i;
            myButton.GetComponent<ButtonCharSkin>().action += ChangePant;
        }

    }
    private void ChangePant(int index, RectTransform pos)
    {
        currentIndex = index;
        SkinData Pant = DataManager.Instance.panDatas[index];
        this.currentPant = Pant.Material;
        player.PanType.material = Pant.Material;
        Description.text = Pant.Description;
        FrameChooseItem.transform.SetParent(pos, false);
        if (SaveManager.Instance.listBoughtPantID.Contains(index))
        {
            ChangeStateSelect();
            return;
        }
        else
        {
            Price.text = Pant.Price.ToString();
        }
    }

    private void BuyOrSelectItem()
    {
        if (string.Equals(Price.text, Constants.equipedStringBtn))
        {
            return;
        }
        if (string.Equals(Price.text, Constants.selectStringBtn))
        {

            ChangeItem();
            SaveIDItem();
            ChangeStateSelect();
            UpdateData();
            return;
        }
        if (GameManager.Instance.Coin >= int.Parse(Price.text))
        {
            GameManager.Instance.UpdateCoin(-int.Parse(Price.text));
            ChangeItem();
            SaveIDItem();
            AddNewItem();
            ChangeStateSelect();
            UpdateData();
            UIManager.Instance.UpDateCoinText();

        }
    }


    private void ChangeItem()
    {
        switch (shopState)
        {
            case ShopState.Pant:             
                player.pantCurrent = this.currentPant;
                break;
            case ShopState.Hat:
                if (player.hatCurrent != null)
                    {
                        Destroy(player.hatCurrent);
                    }
                if (currentHat != null)
                    {
                        player.hatCurrent = Instantiate(currentHat, player.HatPoint);
                        Destroy(currentHat);
                    }
                break;
    }

    }
    private void ChangeStateSelect()
    {

        switch (shopState)
        {
            case ShopState.Hat:
                Price.text = SaveManager.Instance.currentHat == currentIndex ? "Equiped" : "Select";
                break;
            case ShopState.Pant:
                Price.text = SaveManager.Instance.currentPant == currentIndex ? "Equiped" : "Select";
                break;
            default:
                break;
        }
    }
    private void SaveIDItem()
    {
        switch (shopState)
        {
            case ShopState.Hat:
                SaveManager.Instance.currentHat = currentIndex;
                break;
            case ShopState.Pant:
                SaveManager.Instance.currentPant = currentIndex;
                break;
            
        }
    }
    private void AddNewItem()
    {
        switch (shopState)
        {
            case ShopState.Hat:
                SaveManager.Instance.listBoughtHatID.Add(currentIndex);
                break;
            case ShopState.Pant:
                SaveManager.Instance.listBoughtPantID.Add(currentIndex);
                break;
            
        }
    }
    private void UpdateData()
    {
        player.ResetData();
    }
}
