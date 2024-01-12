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
        Select.onClick.AddListener(() => LevelManager.Instance.changWeaponPlayer(UIManager.Instance.GetCurrentWeapon(weapon_index)));
        
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
        if(weapon.weaponData.Price == 0)
        {
            Price.text = "Select";
        }
        else
        {
            Price.text = weapon.weaponData.Price.ToString();
        }

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
