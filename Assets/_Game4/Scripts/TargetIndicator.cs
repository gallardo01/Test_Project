using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{

    [SerializeField] Image iconLevel;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] RectTransform rect;

    Transform target;
    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height) / 2;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(CanvasGame.Instance.getCanvas().transform);
    }

    // Update is called once per frame
    void Update()
    {
        viewPoint = Camera.main.WorldToScreenPoint(target.position) - screenHalf;   
        rect.anchoredPosition = viewPoint;    
    }

    public void OnInit(Transform target)
    {
        this.target = target;
        Color color = new Color(Random.value, Random.value, Random.value, 1);
        iconLevel.color = color;
        nameText.color = color;
    }
}
