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
    [SerializeField] private List<Button> buttonLevels = new List<Button>();   
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
        if(SceneManager.GetActiveScene().name == "MainMenu"){
            for(int i = 0; i < buttonLevels.Count; i++){
                if(i < PlayerPrefs.GetInt("DiamondScene")){
                    buttonLevels[i].gameObject.SetActive(true);
                }
                else{
                    buttonLevels[i].gameObject.SetActive(false);
                }
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

    public void ResetGame(){
        PlayerPrefs.SetInt("DiamondScene", 1);
        SceneManager.LoadScene(0);
    }

    public void UpdateDiamondScore(){
        diamondScore++;
    }

    private void UpdateDiamondUI(){
        diamondText.text = $"{diamondScore}";
    }

    public void GetLevel(int sceneNumber){
        // if(PlayerPrefs.GetInt("DiamondScene") > sceneNumber){    
            SceneManager.LoadScene(sceneNumber);
        // }    
    }

}
