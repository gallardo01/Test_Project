using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class WeaponShop : MonoBehaviour
{
    [SerializeField] Button Back;
    [SerializeField] Button Next;
    [SerializeField] Button Select;
    [SerializeField] Button Buy;
    [SerializeField] Button Exit;
    [SerializeField] TextMeshProUGUI NameWeapon;
    [SerializeField] TextMeshProUGUI Desciption;
    [SerializeField] TextMeshProUGUI Price;
    [SerializeField] TextMeshProUGUI State;

    [SerializeField] Transform WeaponParent;

    private int weapon_index = 0;
    private int total_weapon = 0;
    private Weapon weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon_index = PlayerPrefs.GetInt("Weapon");
        total_weapon = UIManager.Instance.total_weapon;
        InitWeapon(weapon_index);
        Back.onClick.AddListener(() => PreviousWeapon());
        Next.onClick.AddListener(() => NextWeapon());
        Exit.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.MainMenu));
        Select.onClick.AddListener(() => ChangWeaponInShop());
        
    }

    private void InitWeapon(int index)
    {
        foreach (Transform child in WeaponParent)
        {
            Destroy(child.gameObject);
        }
        weapon = Instantiate(UIManager.Instance.GetCurrentWeapon(weapon_index), WeaponParent.position, Quaternion.identity, WeaponParent).GetComponent<Weapon>();
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        NameWeapon.text = weapon.weaponData.NameWeapon;
        Desciption.text = weapon.weaponData.Description;
        OnButton(weapon);

    }
    public void OnButton(Weapon weapon)
    {
        if(weapon.weaponData.Price > 0)
        {
            Buy.gameObject.SetActive(true);
            Select.gameObject.SetActive(false);
            Price.text = weapon.weaponData.Price.ToString();
        }
        else
        {
            Buy.gameObject.SetActive(false);
            Select.gameObject.SetActive(true);
            State.text = LevelManager.Instance.player.typeWeapon == weapon.weaponType ? "Equiped" : "Select"; 
        }

    }
    private void ChangWeaponInShop()
    {
        
        State.text = "Equiped";
        PlayerPrefs.SetInt("Weapon", weapon_index);       
        LevelManager.Instance.player.ChangeWeaponImg();
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
