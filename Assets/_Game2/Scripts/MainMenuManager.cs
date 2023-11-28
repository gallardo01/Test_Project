using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button backButtonPlay;
    [SerializeField] private Button backButtonShop;

    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject PlayPanel;
    [SerializeField] private GameObject ShopPanel;

    private int diamondOwned;
    [SerializeField] private TMP_Text diamondOwnerText;
    [SerializeField] private List<Button> buttonBuy = new List<Button>();
    [SerializeField] private List<TMP_Text> textBuy = new List<TMP_Text>();
    private int[] textPrices = {100, 500, 1000, 2000, 5000};
    
    [SerializeField] private List<Button> buttonLevels = new List<Button>(); 

    public enum MainMenuState{
        Menu,
        Play, 
        Shop
    }


    // Start is called before the first frame update
    void Start()
    {
        InitButtonLevel();
        InitItemPrice();
        InitButton(MainMenuState.Menu);
        playButton.onClick.AddListener(() => InitButton(MainMenuState.Play));
        shopButton.onClick.AddListener(() => InitButton(MainMenuState.Shop));
        backButtonPlay.onClick.AddListener(() => InitButton(MainMenuState.Menu));
        backButtonShop.onClick.AddListener(() => InitButton(MainMenuState.Menu));

        buttonLevels[0].onClick.AddListener(() => InitLevel(0+1));
        buttonLevels[1].onClick.AddListener(() => InitLevel(1+1));
        buttonLevels[2].onClick.AddListener(() => InitLevel(2+1));
        buttonLevels[3].onClick.AddListener(() => InitLevel(3+1));
        buttonLevels[4].onClick.AddListener(() => InitLevel(4+1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitButtonLevel(){
        for(int i = 0; i < buttonLevels.Count; i++){
            if(i < PlayerPrefs.GetInt("DiamondScene")){
                buttonLevels[i].gameObject.SetActive(true);
            }
            else{
                buttonLevels[i].gameObject.SetActive(false);
            }
        }
    }

    private void InitItemPrice(){
        if(!PlayerPrefs.HasKey("DiamondOwned")){
            PlayerPrefs.SetInt("DiamondOwned", 1000);
        }
        diamondOwned = PlayerPrefs.GetInt("DiamondOwned");
        diamondOwnerText.text = $"{diamondOwned}";
        for(int i = 0 ; i < textPrices.Length; i++){
            if(textPrices[i] <= diamondOwned){
                textBuy[i].text = $"{textPrices[i]}";
                buttonBuy[i].gameObject.SetActive(true);
            }
            else{
                buttonBuy[i].gameObject.SetActive(false);
            }
        }
    }

    private void InitButton(MainMenuState state){
        if(state == MainMenuState.Menu){
            MainPanel.gameObject.SetActive(true);
            PlayPanel.gameObject.SetActive(false);
            ShopPanel.gameObject.SetActive(false);
        }
        else if(state == MainMenuState.Play){
            MainPanel.gameObject.SetActive(false);
            PlayPanel.gameObject.SetActive(true);
            ShopPanel.gameObject.SetActive(false);
        }
        else if(state == MainMenuState.Shop){
            MainPanel.gameObject.SetActive(false);
            PlayPanel.gameObject.SetActive(false);
            ShopPanel.gameObject.SetActive(true);
        }
    }

    private void InitLevel(int level){
        SceneManager.LoadScene(level);
    }


}
