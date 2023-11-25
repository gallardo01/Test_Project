using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void Level()
    {
        SceneManager.LoadScene("level");
    }
    public void Shop()
    {
        SceneManager.LoadScene("shop");
    }
    public void level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void level3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void level4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void level5()
    {
        SceneManager.LoadScene("Level5");
    }
}
