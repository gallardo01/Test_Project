using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponShop : MonoBehaviour
{
    [SerializeField] Button next;
    [SerializeField] Button previous;
    [SerializeField] Button equip;
    [SerializeField] TextMeshProUGUI equipText;

    [SerializeField] Button pick;
    [SerializeField] Button back;
    [SerializeField] Transform parent;
    [SerializeField] private GameObject mainMenu;

    private int weapon_index = 0;
    private int total_weapon = 0;
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        weapon_index = PlayerPrefs.GetInt("Weapon");
        total_weapon = GameManager.Instance.total_weapon;
        InitWeapon(weapon_index);
        next.onClick.AddListener(() => NextWeapon());
        previous.onClick.AddListener(() => PreviousWeapon());
        back.onClick.AddListener(() => BackWeapon());
        equip.onClick.AddListener(() => EquipWeapon());

    }

    private void InitWeapon(int index)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
        weapon = Instantiate(GameManager.Instance.GetCurrentWeapon(weapon_index), parent.position, Quaternion.identity, parent);
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

    private void BackWeapon(){
        gameObject.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void EquipWeapon()
    {
        equipText.text = "Equiped";
        equip.GetComponent<Image>().color = new Color(0f, 255f, 0f);
        // Them bien trang thai equip/unequip
    }
}