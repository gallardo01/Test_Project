using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void ChooseLevel()
    {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Stage"))
        {
            PlayerPrefs.SetInt("Stage", 3);
        }
        if (PlayerPrefs.GetInt("Stage") == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        if (PlayerPrefs.GetInt("Stage") == 2)
        {
            SceneManager.LoadScene("Level2");
        }
        if (PlayerPrefs.GetInt("Stage") == 3)
        {
            SceneManager.LoadScene("Level3");
        }
    }
}
