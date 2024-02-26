using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GiftCode : CanvasAbs
{
    public TMP_InputField inputField;
    [SerializeField] private Button Exit;
    [SerializeField] private Button Enter;
    private void Start()
    {
        Exit.onClick.AddListener(() => BackToMainMenu());
        Enter.onClick.AddListener(() => CheckCode());
    }
    public override void BackToMainMenu()
    {
        base.BackToMainMenu();
    }

    public void CheckCode()
    {
        string code = inputField.text.Trim();
        if(code.Equals(Constants.giftCode))
        {
            GameManager.Instance.UpdateCoin(4000);
        }
        inputField.text = "";
        this.BackToMainMenu();
    }

}
