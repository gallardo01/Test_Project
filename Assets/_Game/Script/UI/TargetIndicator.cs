using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class TargetIndicator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textName;
    [SerializeField] private RectTransform rect;
    [SerializeField] private Image icon;
    [SerializeField] private Image arrow;
    [SerializeField] private GameObject follow;

    Vector3 viewPoint_t;
    Vector3 screenHalf = new Vector2(Screen.width, Screen.height)/2;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(CanvasGame.Instance.canvasTranForm().transform);
    }

    // Update is called once per frame
    void Update()
    {
        float minX = rect.rect.width / 2;
        float maxX = Screen.width - minX;

        float minY = rect.rect.width / 2;
        float maxY = Screen.height - minY;

        Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        Vector2 direction = (Vector2)pos - new Vector2(Screen.width / 2, Screen.height / 2);
        float angle = Vector2.Angle(direction, Vector3.right);
        if (direction.y <= 0) angle = 360 - angle;

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        if (pos.x == maxX || pos.y == maxY || pos.x == minX || pos.y == minY)
        {
            arrow.gameObject.SetActive(true);
            follow.transform.eulerAngles = Vector3.forward * angle;
        }
        else
        {
            arrow.gameObject.SetActive(false);
             follow.transform.eulerAngles = Vector3.zero;
        }

        this.transform.position = pos;

    }

    public void setScore(int score)
    {
        textScore.text = score.ToSafeString();
    }
    public void OnInit(Transform target, string name) {
        this.target = target;  
        Color color = new Color(Random.value, Random.value, Random.value, 1);
        icon.color = color;
        textName.color = color;
        textName.text = name;
    }
}

