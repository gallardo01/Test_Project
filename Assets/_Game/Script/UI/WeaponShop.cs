using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UI;


public class WeaponShop : CanvasAbs
{
    [SerializeField] Button Back;
    [SerializeField] Button Next;

    [SerializeField] Button Exit;
    [SerializeField] TextMeshProUGUI NameWeapon;
    [SerializeField] TextMeshProUGUI Desciption;
    [SerializeField] Button Select;
    [SerializeField] TextMeshProUGUI Price;

    [SerializeField] Transform WeaponParent;

    private int weapon_index;
    private int total_weapon = 0;
    public Weapon weapon;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        this.player = LevelManager.Instance.player;
        weapon_index = SaveManager.Instance.currentWeapon;
        total_weapon = LevelManager.Instance.weapons.Count;
        InitWeapon(weapon_index);
        Back.onClick.AddListener(() => PreviousWeapon());
        Next.onClick.AddListener(() => NextWeapon());
        Exit.onClick.AddListener(() => BackToMainMenu());
        Select.onClick.AddListener(() => SelectOrBuyWeapon());
        
    }

    public override void BackToMainMenu()
    {
        base.BackToMainMenu();
    }
    private void InitWeapon(int index)
    {
        foreach (Transform child in WeaponParent)
        {
            Destroy(child.gameObject);
        }
        weapon = Instantiate(LevelManager.Instance.GetCurrentWeapon(weapon_index), WeaponParent.position, Quaternion.identity, WeaponParent).GetComponent<Weapon>();
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        NameWeapon.text = weapon.weaponData.NameWeapon;
        Desciption.text = weapon.weaponData.Description;
        if (SaveManager.Instance.listBoughtWeaponID.Contains(index))
        {
            ChangeStateSelect(index);
            return;
        }
        Price.text = weapon.weaponData.Price.ToString();


    }

    private void SelectOrBuyWeapon()
    {
        if(string.Equals(Price.text, Constants.equipedStringBtn)){
            return;
        }
        if (string.Equals(Price.text, Constants.selectStringBtn))
        {

            SaveManager.Instance.currentWeapon = weapon_index;
            UpdateDataWeapon();
            ChangeStateSelect(weapon_index);
            return;
        }
        if (GameManager.Instance.Coin >= int.Parse(Price.text))
        {
            GameManager.Instance.UpdateCoin(-int.Parse(Price.text));
            SaveManager.Instance.currentWeapon = weapon_index;
            UpdateDataWeapon();
            ChangeStateSelect(weapon_index);
            SaveManager.Instance.listBoughtWeaponID.Add(weapon_index);
            UIManager.Instance.UpDateCoinText();

        }
    }
    private void UpdateDataWeapon()
    {
        this.player.typeWeapon = LevelManager.Instance.weapons[weapon_index].weaponType;
        this.player.ResetData();
    }
    private void ChangeStateSelect(int index)
    {
        Price.text = SaveManager.Instance.currentWeapon == index ? "Equiped" : "Select";
    }
    private void NextWeapon()
    {
       
        weapon_index++;
        if (weapon_index == total_weapon)
        {
            weapon_index = 0;
        }
        InitWeapon(weapon_index);
        
    }

    private void PreviousWeapon()
    {
        
        weapon_index--;
        if (weapon_index < 0)
        {
            weapon_index = total_weapon - 1;
        }
        InitWeapon(weapon_index);
        
    }
}
