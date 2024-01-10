using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    // Shop
    [Header("Shop")]
    [SerializeField] private Button nextItem;
    [SerializeField] private Button previousItem;
    [SerializeField] private Transform itemPlace;
    [SerializeField] private TextMeshProUGUI itemName, itemPrice, itemEffect;
    [SerializeField] private WeaponList weapons;
    
    private int currentWeapon;
    private Weapon weapon;

    [Header("Other")]
    [SerializeField] TextMeshProUGUI remainingCount;

    private void Start()
    {
        // Shop
        nextItem.onClick.AddListener(() => {Next();});
        previousItem.onClick.AddListener(() => {Previous();});
        currentWeapon = 1;

        weapon = Instantiate(weapons.GetWeapon(currentWeapon), itemPlace);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        
    }

    private void Update()
    {
        // remainingCount.SetText("Count: " + LevelManager.Instance.remainingBotCount);
    }

    private void Next()
    {
        currentWeapon = currentWeapon == weapons.Size - 1 ? currentWeapon : currentWeapon + 1;
        CreateWeapon();
    }

    private void Previous()
    {
        currentWeapon = currentWeapon == 0 ? currentWeapon : currentWeapon - 1;
        CreateWeapon();
    }

    private void CreateWeapon() {
        itemName.SetText(weapons.GetWeapon(currentWeapon).WeaponName);
        Destroy(weapon.gameObject);
        weapon = Instantiate(weapons.GetWeapon(currentWeapon), itemPlace);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
    }
}
