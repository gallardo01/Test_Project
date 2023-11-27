using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Button play, shop;
    [SerializeField] private GameObject pmenu, plevel, pshop;
    [SerializeField] private Button back;

    private GameObject current;

    private void Start() {
        current = pmenu;
        play.onClick.AddListener(() => {
            changeView(plevel);
        });

        shop.onClick.AddListener(() => {
            changeView(pshop);
        });

        back.onClick.AddListener(() => {
            changeView(pmenu);
        });
    }

    private void changeView(GameObject newPanel) {
        current.SetActive(false);
        current = newPanel;
        current.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
