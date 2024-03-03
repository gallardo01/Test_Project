using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] Player player;

    private List<int> listBoughtPantID = new List<int>();
    public List<int> ListBoughtPantID { get => listBoughtPantID; set 
        {
            listBoughtPantID = value;
            SaveGame();
        }
    }
    private List<int> listBoughtHatID = new List<int>();
    public List<int> ListBoughtHatID
    {
        get => listBoughtHatID; set
        {
            listBoughtHatID = value;
            SaveGame();
        }
    }
    private List<int> listBoughtShieldID;
    public List<int> ListBoughtShieldID
    {
        get => listBoughtShieldID; set
        {
            listBoughtShieldID = value;
            SaveGame();
        }
    }
    private List<int> listBoughtWeaponID = new List<int>();
    public List<int> ListBoughtWeaponID
    {
        get => listBoughtWeaponID; set
        {
            listBoughtWeaponID = value;
            SaveGame();
            Debug.Log("listWeapon");
        }
    }
    private int currentHat;
    public int CurrentHat { get => currentHat; set 
        {
            currentHat = value;
            SaveGame();
        } 
    }
    private int currentPant;
    public int CurrentPant
    {
        get => currentPant; set
        {
            currentPant = value;
            SaveGame();
        }
    }
    private int currentWeapon;
    public int CurrentWeapon
    {
        get => currentWeapon; set
        {
            currentWeapon = value;
            SaveGame();
            Debug.Log("currentweapon");
        }
    }
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        LoadSave();
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
        PlayerPrefs.SetInt("Weapon", CurrentWeapon);
        PlayerPrefs.SetInt("Hat", CurrentHat);
        PlayerPrefs.SetInt("Pant", CurrentPant);
        PlayerPrefs.SetString("ListWeapon", string.Join(",", ListBoughtWeaponID));
        PlayerPrefs.SetString("ListHat", string.Join(",", ListBoughtHatID));
        PlayerPrefs.SetString("ListPant", string.Join(",", ListBoughtPantID));
    }
}
