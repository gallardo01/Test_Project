using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] Button Back;
    [SerializeField] Button Next;
    [SerializeField] Button Select;
    [SerializeField] Button Buy;
    [SerializeField] Button Exit;

    [SerializeField] Transform WeaponParent;

    private int weapon_index = 0;
    private int total_weapon = 0;
    private GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon_index = PlayerPrefs.GetInt("Weapon");
        total_weapon = UIManager.Instance.total_weapon;
        InitWeapon(weapon_index);
        Back.onClick.AddListener(() => PreviousWeapon());
        Next.onClick.AddListener(() => NextWeapon());
        Exit.onClick.AddListener(() => UIManager.Instance.OpenCanvasUI(GameState.MainMenu));
        
    }

    private void InitWeapon(int index)
    {
        foreach (Transform child in WeaponParent)
        {
            Destroy(child.gameObject);
        }
        weapon = Instantiate(UIManager.Instance.GetCurrentWeapon(weapon_index), WeaponParent.position, Quaternion.identity, WeaponParent);
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
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
