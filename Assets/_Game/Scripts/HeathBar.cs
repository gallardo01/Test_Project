using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;

    float hp;
    float maxHP;
    private Transform target;
    // Update is called once per frame
    void Update()
    {
        //imageFill.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxHP, Time.deltaTime * 5f);
        transform.position = target.position + offset;
    }
    public void OnInit(float maxHP, Transform target)
    {
        
        this.target = target;
        this.maxHP = maxHP;
        hp = maxHP;
        imageFill.fillAmount = 1;
    }
    public void setnewHp(float hp)
    {
        this.hp = hp;
    }
}
