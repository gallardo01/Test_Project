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

    bool viewState = true;
    Vector3 positionVector = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (viewState)
        {
            viewPoint = Camera.main.WorldToScreenPoint(target.position) - screenHalf;
            rect.anchoredPosition = viewPoint / (Screen.width / 1080f);
        } else
        {

        }
    }

    public void OutOfRange(bool state, Vector3 vector)
    {
        viewState = state;
        positionVector = vector;
    }

    public void OutOfRange(bool state)
    {
        viewState = state;
    }
    public void OnInit(Transform target)
    {
        this.target = target;
        Color color = new Color(Random.value, Random.value, Random.value, 1);
        iconLevel.color = color;
        nameText.color = color;
    }

    public void SetScore(int score)
    {
        levelText.text = score.ToString();
    }

    public void SetInformation(string name)
    {
        nameText.text = name;
    }
}
