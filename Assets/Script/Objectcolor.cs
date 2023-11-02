using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectcolor : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(taskonclick);
    }

    // Update is called once per frame
    void Update()
    {
        button.onClick.AddListener(taskonclick);
    }

    void taskonclick()
    {
        var cuberender = gameObject.GetComponent<Renderer>();
        Color color = new Color(Random.Range(0,5), Random.Range(0,5), Random.Range(0, 5));
        cuberender.material.SetColor("_Color", color);
    }
}
