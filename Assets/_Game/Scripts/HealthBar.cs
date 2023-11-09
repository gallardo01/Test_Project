using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;

    private float hp;
    private float maxHp;
    

    private void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHp, Time.deltaTime * 4f);
        transform.position = target.position + offset;
    }

    public void OnInit(float maxHp) {
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1;
    }

    private void OnDestroy() {
    }

    public void SetNewHp(float hp) {
        this.hp = hp;
    }
}
