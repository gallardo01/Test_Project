using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject ButtonPanel, PlayGamePanel, ShopPanel; 
    public Button playButton, shopButton, backButtonPlay, backButtonShop;
    int diamond;

    public enum MainMenuState
    {
        Menu,
        Game,
        Shop,
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Diamond"))
        {
            PlayerPrefs.SetInt("Diamond", 0);
        }
        else
        {
            diamond = PlayerPrefs.GetInt("Diamond");
        }
        
        initPanel(MainMenuState.Menu);
        playButton.onClick.AddListener(() => initPanel(MainMenuState.Game));
        shopButton.onClick.AddListener(() => initPanel(MainMenuState.Shop));
        backButtonPlay.onClick.AddListener(() => initPanel(MainMenuState.Menu));
        backButtonShop.onClick.AddListener(() => initPanel(MainMenuState.Menu));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initPanel(MainMenuState state)
    {
        if (state == MainMenuState.Menu)
        {
            ButtonPanel.SetActive(true);
            PlayGamePanel.SetActive(false);
            ShopPanel.SetActive(false);
        }
        else if (state == MainMenuState.Game)
        {
            ButtonPanel.SetActive(false);
            PlayGamePanel.SetActive(true);
            ShopPanel.SetActive(false);
        }
        else if (state == MainMenuState.Shop)
        {
            ButtonPanel.SetActive(false);
            PlayGamePanel.SetActive(false);
            ShopPanel.SetActive(true);
        }
    }
}
