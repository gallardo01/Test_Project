using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncreaseScore : MonoBehaviour
{

    [SerializeField] private Sprite[] fonts;

    [SerializeField] private Image one, two, three;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init(int value) {
        three.overrideSprite = fonts[0];
        two.overrideSprite = fonts[value / 10 % 10];
        one.overrideSprite = fonts[value / 100 % 10];
        Show();
    }

    public void Hide() {
        gameObject.SetActive(false);
    }

    public void Show() {
        gameObject.SetActive(true);
        Invoke(nameof(Hide), 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
