using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] Button next;
    [SerializeField] Button previous;
    [SerializeField] Button equiped;
    [SerializeField] Button pick;
    [SerializeField] Button back;
    [SerializeField] Transform parent;

    [SerializeField] GameObject player;
    private int weapon_index = 0;
    private int total_weapon = 0;
    private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon_index = PlayerPrefs.GetInt("Weapon");
        total_weapon = GameController.Ins.total_weapon;
        InitWeapon(weapon_index);
        next.onClick.AddListener(() => NextWeapon());
        previous.onClick.AddListener(() => PreviousWeapon());
        pick.onClick.AddListener(() => ChangeWeapon());
    }

    private void ChangeWeapon()
    {
        PlayerPrefs.SetInt("Weapon", weapon_index);
        player.GetComponent<Character>().ChangeWeapon(weapon_index);
        InitWeapon(weapon_index);
    }
    private void InitWeapon(int index)
    {
        weapon_index = index;
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        // ng choi dang deo vu khi do
        if (index == PlayerPrefs.GetInt("Weapon"))
        {
            equiped.gameObject.SetActive(true);
            pick.gameObject.SetActive(false);
        }
        else
        {
            equiped.gameObject.SetActive(false);
            pick.gameObject.SetActive(true);
        }
        weapon = Instantiate(GameController.Ins.GetCurrentWeapon(weapon_index), parent.position, Quaternion.identity, parent);
        weapon.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void NextWeapon()
    {
        weapon_index++;
        if(weapon_index == total_weapon)
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
