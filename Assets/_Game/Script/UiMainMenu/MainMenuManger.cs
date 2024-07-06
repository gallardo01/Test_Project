using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuManger : MonoBehaviour
{
    public GameObject btnPanel;
    public GameObject playGamePanel;
    public GameObject shopPanel;

    public Button playBtn;
    public Button shopBtn;
    public Button backBtnPlay;
    public Button backBtnShop;
    //public Button lv1;

    public TextMeshProUGUI diamondText;
    private int diamond = 0;

    public Button[] buyItemShop;
    public TextMeshProUGUI[] priceItemShop;
    private static int[] ItemPrice = {100, 500, 1000, 2000, 5000};

    public enum MainMenuState{
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
		initBtnShop();
        diamondText.text = diamond.ToString();
        initPanel(MainMenuState.Menu);
        playBtn.onClick.AddListener(() => initPanel(MainMenuState.Game));
        shopBtn.onClick.AddListener(() => initPanel(MainMenuState.Shop));
        backBtnPlay.onClick.AddListener(() => initPanel(MainMenuState.Menu));
        backBtnShop.onClick.AddListener(() => initPanel(MainMenuState.Menu));

        //lv1.onClick.AddListener(() => loadSence(1));


    }
    private void loadSence(int sceneId){
        SceneManager.LoadScene(1);
    }
    private void initBtnShop()
    {
        for(int i = 0; i <5; i++){
			int index = i;
			priceItemShop[i].text = ItemPrice[i].ToString();
            if (diamond>= ItemPrice[i])
            {
                buyItemShop[i].gameObject.SetActive(true);
				buyItemShop[i].onClick.RemoveAllListeners();
				buyItemShop[i].onClick.AddListener(() => BuyItem(index));
			}
            else
            {
                buyItemShop[i].gameObject.SetActive(false);
            }
        }
    }
    private void initPanel(MainMenuState state)
    {
        if (state == MainMenuState.Menu)
        {
            btnPanel.SetActive(true);
            playGamePanel.SetActive(false);
            shopPanel.SetActive(false);
        }
        else if(state == MainMenuState.Shop)
        {
            btnPanel.SetActive(false);
            playGamePanel.SetActive(false);
            shopPanel.SetActive(true);
        }
        else if (state == MainMenuState.Game)
        {
            btnPanel.SetActive(false);
            playGamePanel.SetActive(true);
            shopPanel.SetActive(false);
        }
    }
	private void BuyItem(int index)
	{
		if (diamond >= ItemPrice[index])
		{
            diamond -= ItemPrice[index];
			PlayerPrefs.SetInt("Diamond", diamond);
			diamondText.text = diamond.ToString();
			initBtnShop();
		}
	}

	// Update is called once per frame
	void Update()
    {
        
    }
}
