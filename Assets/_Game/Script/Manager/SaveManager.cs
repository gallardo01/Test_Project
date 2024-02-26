using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] Player player;

    public List<int> listBoughtPantID;
    public List<int> listBoughtHatID;
    public List<int> listBoughtShieldID;
    public List<int> listBoughtWeaponID;
    public int currentHat;
    public int currentPant;
    public int currentWeapon;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        LoadSave();
    }
    private void OnApplicationQuit()
    {
        this.SaveGame();
    }
    private void LoadSave()
    {


        currentHat = LoadCurrentItem("Hat");
        currentPant = LoadCurrentItem("Pant");

        LoadListBoughtItem("ListHat", listBoughtHatID);
        LoadListBoughtItem("ListPant", listBoughtPantID);
        LoadListBoughtItem("ListWeapon",listBoughtWeaponID);


        if (LoadCurrentItem("Weapon") == -1)
        {
            currentWeapon = 0;
            listBoughtWeaponID.Add(0);
            PlayerPrefs.SetInt("Weapon", 0);
        }
        else
        {
            currentWeapon = LoadCurrentItem("Weapon");
            if (listBoughtWeaponID.Contains(currentWeapon))
            {
                return;
            }
            listBoughtWeaponID.Add(currentWeapon);
        }
    } 
    private void LoadListBoughtItem(string ListName, List<int> ListBought)
    {
        string ListItem = PlayerPrefs.GetString(ListName, "");
        if (!string.IsNullOrEmpty(ListItem))
        {
            string[] StringID = ListItem.Split(',');
            foreach (string number in StringID)
            {
                int ID;
                if (int.TryParse(number, out ID))
                {
                    if (ListBought.Contains(ID))
                    {
                        return;
                    }
                    ListBought.Add(ID);
                }
            }
        }
    }
    private int LoadCurrentItem(string item)
    {
        if (!PlayerPrefs.HasKey(item))
        {
            PlayerPrefs.SetInt(item, -1);
        }
        else
        {
             PlayerPrefs.GetInt(item);
        }
        return PlayerPrefs.GetInt(item);
    }

    public void SaveGame()
    {
        PlayerPrefs.SetInt("Coin", GameManager.Instance.Coin);
        PlayerPrefs.SetInt("Weapon", currentWeapon);
        PlayerPrefs.SetInt("Hat", currentHat);
        PlayerPrefs.SetInt("Pant", currentPant);
        PlayerPrefs.SetString("ListWeapon", string.Join(",", listBoughtWeaponID));
        PlayerPrefs.SetString("ListHat", string.Join(",", listBoughtHatID));
        PlayerPrefs.SetString("ListPant", string.Join(",", listBoughtPantID));
    }
}
