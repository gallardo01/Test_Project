using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject buttonPanel;
    public GameObject playGamePanel;
    public GameObject shopPanel;

    public Button playButton;
    public Button shopButton;
    public Button backButtonPlay;
    public Button backButtonShop;

    public TextMeshProUGUI diamondText;
    private int diamond = 0;

    public Button[] buyItemShop;
    public TextMeshProUGUI[] priceItemShop;
    private static int[] ItemPrice = {100, 500, 1000, 2000, 5000 };
    public enum MainMenuState
    {
        Menu,
        Game,
        Shop
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 1000);
        } 
        diamond = PlayerPrefs.GetInt("Diamond");
        diamondText.text = diamond.ToString();
        initButtonShop();
        initPanel(MainMenuState.Menu);
        playButton.onClick.AddListener(() => initPanel(MainMenuState.Game));
        shopButton.onClick.AddListener(() => initPanel(MainMenuState.Shop));
        backButtonPlay.onClick.AddListener(() => initPanel(MainMenuState.Menu));
        backButtonShop.onClick.AddListener(() => initPanel(MainMenuState.Menu));

        //level1Button.onClick.AddListener(() => loadScene(5));

        PlayerPrefs.SetInt("Brick", 1);
    }

    private void initButtonShop()
    {
        for (int i = 0; i < 5; i++)
        {
            priceItemShop[i].text = ItemPrice[i].ToString();
            if (diamond >= ItemPrice[i])
            {
                buyItemShop[i].gameObject.SetActive(true);
            }
            else
            {
                buyItemShop[i].gameObject.SetActive(false);
            }
        }
    }

    private void loadScene(int sceneId)
    {
        SceneManager.LoadScene(1);
    }

    private void initPanel(MainMenuState state)
    {
        if (state == MainMenuState.Menu)
        {
            buttonPanel.SetActive(true);
            playGamePanel.SetActive(false);
            shopPanel.SetActive(false);
        } else if (state == MainMenuState.Shop)
        {
            buttonPanel.SetActive(false);
            playGamePanel.SetActive(false);
            shopPanel.SetActive(true);
        }
        else if (state == MainMenuState.Game)
        {
            buttonPanel.SetActive(false);
            playGamePanel.SetActive(true);
            shopPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
