using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonCharSkin : MonoBehaviour
{
    public UnityAction<int, RectTransform> action;
    public int index;
    private RectTransform rectTransform;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        this.GetComponent<Button>().onClick.AddListener(()=> OnClick());
    }
    private void OnClick()
    {
        action?.Invoke(index, rectTransform);
    }
}
