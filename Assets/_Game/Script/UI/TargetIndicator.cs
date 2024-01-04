using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private RectTransform rect;
    [SerializeField] private Image icon;

    Vector3 viewPoint;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height)/2;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(CanvasGame.Instance.canvasTranForm().transform);
    }

    // Update is called once per frame
    void Update()
    {
        
            if(Camera.main != null)
        {
            viewPoint = Camera.main.WorldToScreenPoint(target.position) - screenHalf;
            rect.anchoredPosition = viewPoint;
        }

    }
    public void OnInit(Transform target) {
        this.target = target;  
        Color color = new Color(Random.value, Random.value, Random.value, 1);
        icon.color = color;
        textName.color = color;
    }
}
