using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{

    [SerializeField] private Image texture;
    [SerializeField] private Button buy;

    // Start is called before the first frame update
    void Start()
    {
        buy.onClick.AddListener(() =>
        {
            CurrentTile.Instance.currentTexture = texture.mainTexture;
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
