using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject weaponCanvas;
    [SerializeField] GameObject[] weapons;
    private int weapon_index = 0;
    public int total_weapon => weapons.Length;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Weapon"))
        {
            PlayerPrefs.SetInt("Weapon", 0);
        } else
        {
            weapon_index = PlayerPrefs.GetInt("Weapon");
        }
    }

    public GameObject GetCurrentWeapon(int index)
    {
        return weapons[index];
    }

    public void PlayGame()
    {
        joystick.SetActive(true);
        gameCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void ChangeWeapon()
    {
        joystick.SetActive(false);
        gameCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
        weaponCanvas.SetActive(true);
    }
}
