using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI diamond;

    // Start is called before the first frame update
    void Start()
    {
        diamond.SetText(PlayerPrefs.GetInt("diamondCount", 0).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
