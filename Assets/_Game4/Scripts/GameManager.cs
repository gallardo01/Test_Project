using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject joyStick;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject weaponCanvas;
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private TextMeshProUGUI endGameText;


    [SerializeField] GameObject[] weapons;
    private int weapon_index = 0;
    public int total_weapon => weapons.Length;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Weapon")){
            PlayerPrefs.SetInt("Weapon", 0);
        }
        else{
            weapon_index = PlayerPrefs.GetInt("Weapon");
        }
    }

    public GameObject GetCurrentWeapon(int index){
        return weapons[index];
    }

    // Update is called once per frame
    public void PlayGame()
    {
        Debug.Log("PlayGame");
        joyStick.SetActive(true);
        gameCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ChangeWeapon(){
        joyStick.SetActive(false);
        gameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        weaponCanvas.SetActive(true);
    }

    public void EndGame(bool status)
    {
        if (status == Status.win)
        {
            endGameText.text = PopUpText.win;
        }
        else
        {
            endGameText.text = PopUpText.lose;
        }

        endGameCanvas.gameObject.SetActive(true);
        joyStick.SetActive(false);
    }

    public void LoadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
    
    public void TryAgain()
    {
        LoadScene();
        
        // endGameCanvas.gameObject.SetActive(false);
        mainMenuCanvas.SetActive(false);
        gameCanvas.SetActive(true);

        // PlayGame();

        // joyStick.SetActive(true);
        // gameCanvas.SetActive(true);
        // mainMenuCanvas.SetActive(false);

        LevelManager.Instance.OnInit();
    }
}
