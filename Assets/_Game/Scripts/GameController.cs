using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    public int diamondScore = 0;
    // public int passedLevel = 0;
    public int levelNumber;
    public TMP_Text diamondText;
    // Start is called before the first frame update
    void Start()
    {
        // if(PlayerPrefs.HasKey("DiamondScore")){
        //     diamondScore = PlayerPrefs.GetInt("DiamondScore");
        // }
        if(!PlayerPrefs.HasKey("DiamondScene")){
            PlayerPrefs.SetInt("DiamondScene", 1);
        }
        else{
            if(PlayerPrefs.GetInt("DiamondScene") < levelNumber){
                PlayerPrefs.SetInt("DiamondScene", levelNumber);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu"){
            UpdateDiamondUI();
        }
        else{
            // if()
        }
    }

    public void UpdateDiamondScore(){
        diamondScore++;
    }

    private void UpdateDiamondUI(){
        diamondText.text = $"{diamondScore}";
    }

    public void GetLevel(int sceneNumber){
        if(PlayerPrefs.GetInt("DiamondScene") > sceneNumber){
            SceneManager.LoadScene(sceneNumber);
        }
    }

}
