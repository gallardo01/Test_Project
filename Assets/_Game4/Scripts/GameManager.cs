using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using MarchingBytes;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject joyStick;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject weaponCanvas;
    [SerializeField] private Canvas endGameCanvas;
    [SerializeField] private TextMeshProUGUI endGameText;
    public bool gameStatus = true;
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
        // Pause(false);
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
        gameStatus = false;
        Invoke(nameof(EndGameMenu.Next), 1f);
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

    public void Pause(bool status)
    {
        if (status)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void JustPause()
    {
        Time.timeScale = 0;
    }
}
