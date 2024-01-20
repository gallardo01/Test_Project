using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{

    [SerializeField] Transform hand;

    GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject ChangeWeapon(int index)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
        currentWeapon = Instantiate(GameController.Ins.GetCurrentWeapon(index), hand);
        return currentWeapon;
    }
}
