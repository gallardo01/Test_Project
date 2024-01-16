using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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


public class SkinShop : MonoBehaviour
{
    [SerializeField] GameObject ButtonPrefab;
    [SerializeField] Button Exit;
    [SerializeField] TextMeshProUGUI Description;
    [SerializeField] Button Select;
    [SerializeField] Button Equiped;
    [SerializeField] Button Buy;
    [SerializeField] TextMeshProUGUI Price;
    [SerializeField] GameObject ChooseItem;




    [SerializeField] List<Button> buttons;
    [SerializeField] List<GameObject> views; 
    public Transform contentHat;
    public Transform contentPan;
    public Transform contentShield;
    public Transform contentFullSet;

    public RectTransform firstHat;
    public RectTransform firstPant;

    public UnityAction<int> onButtonClicked;
    private GameObject currentView;
    private Button currentButton;

    public ShopState shopState;
    public string[] nameShop  = { "Hat", "Pant", "Shield", "FullSet" };
    private void Awake()
    { 
        LoadShopHat();
        LoadShopPan();
        Exit.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.MainMenu));
        Buy.onClick.AddListener(() => BuyItem());
        Select.onClick.AddListener(() => SelectItem());
    }
    void Start()
    { 
        foreach (GameObject view in views)
        {
            view.SetActive(false);
        }
        for (int i = 0; i < buttons.Count; i++)
        {
            int buttonIndex = i;
            buttons[i].GetComponent<Button>().onClick.AddListener(() => OnButtonClick(buttonIndex));
        }
        onButtonClicked += ChangeView;
        shopState = ShopState.Hat;
        ChangeView(0);
    }
    private void SelectItem()
    {
        
    }

    private void BuyItem()
    {
        
    }
    void OnButtonClick(int buttonIndex)
    {
        onButtonClicked?.Invoke(buttonIndex);
    }

    void ChangeView(int buttonIndex)
    {
        if(currentButton != null)
        {
            currentButton.image.enabled = false;
            currentView.SetActive(false);
        }
        if (buttonIndex < views.Count)
        {
            currentButton = buttons[buttonIndex];
            currentButton.image.enabled = true;
            currentView = views[buttonIndex];
            currentView.SetActive(true);
            shopState = (ShopState)buttonIndex;
            ChangFirstItem(buttonIndex);
        }
    }

    private void ChangFirstItem(int index)
    {
        switch(index)
        {
            case 0:
                ChangeHat(0, firstHat); break;
            case 1: 
                ChangePant(1, firstPant); break;

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
        if (!PlayerPrefs.HasKey("Hat" + 0))
        {
            PlayerPrefs.SetInt("Hat" + 0, 0);
        }
        for (int i = 1; i < count; i++)
        {
            GameObject myButton = Instantiate(ButtonPrefab, contentHat);
            myButton.GetComponent<Image>().sprite = DataManager.Instance.hatDatas[i].Image;
            myButton.GetComponent<ButtonCharSkin>().index = i;
            myButton.GetComponent<ButtonCharSkin>().action += ChangeHat;
            if (!PlayerPrefs.HasKey("Hat" + i))
            {
                PlayerPrefs.SetInt("Hat" + i, 0);
            }            
        }

    }

    private void ChangeHat(int index, RectTransform pos)
    {
        //Debug.Log(index);
        foreach (Transform child in LevelManager.Instance.player.HatPoint)
        {
            child.gameObject.SetActive(false);
        }
        SkinData Hat = DataManager.Instance.hatDatas[index];
        Instantiate(Hat.Prefabs, LevelManager.Instance.player.HatPoint);
        Description.text = Hat.Description;
        CheckStateItem(index, "Hat", Hat.Price);
        ChooseItem.transform.SetParent(pos, false);


    }
    private void LoadShopPan()
    {
        int count = DataManager.Instance.panDatas.Count;
        GameObject firstButton = Instantiate(ButtonPrefab, contentPan);
        firstButton.GetComponent<Image>().sprite = DataManager.Instance.panDatas[0].Image;
        firstButton.GetComponent<ButtonCharSkin>().index = 0;
        firstButton.GetComponent<ButtonCharSkin>().action += ChangePant;
        firstPant = (RectTransform)firstButton.gameObject.transform;
        if (!PlayerPrefs.HasKey("Pant" + 0))
        {
            PlayerPrefs.SetInt("Pant" + 0, 0);
        }
        for (int i = 1; i < count; i++)
        {
            GameObject myButton = Instantiate(ButtonPrefab, contentPan);
            myButton.GetComponent<Image>().sprite = DataManager.Instance.panDatas[i].Image;
            myButton.GetComponent<ButtonCharSkin>().index = i;
            myButton.GetComponent<ButtonCharSkin>().action += ChangePant;
            if (!PlayerPrefs.HasKey("Pant" + i))
            {
                PlayerPrefs.SetInt("Pant" + i, 0);
            }
        }

    }
    private void ChangePant(int index, RectTransform pos)
    {
       
        SkinData Pant = DataManager.Instance.panDatas[index];
        LevelManager.Instance.player.PanType.material = DataManager.Instance.panDatas[index].Material;
        Description.text = Pant.Description;
        CheckStateItem(index, "Hat", Pant.Price);
        ChooseItem.transform.SetParent(pos, false);
    }

    private void CheckStateItem(int index, string nameItem, int price)
    {
        switch(PlayerPrefs.GetInt(nameItem + index))
        {
            case 0:
                Select.gameObject.SetActive(false);
                Equiped.gameObject.SetActive(false);
                Buy.gameObject.SetActive(true);
                Price.text = price.ToString();

                break;
            case 1:
                Select.gameObject.SetActive(true);
                Buy.gameObject.SetActive(false);
                Equiped.gameObject.SetActive(false);
                break;
            case 2:
                Select.gameObject.SetActive(false);
                Buy.gameObject.SetActive(false);
                Equiped.gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }

}
