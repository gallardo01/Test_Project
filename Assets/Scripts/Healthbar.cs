using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image imageFilled;
    [SerializeField] Vector3 offset;
    float hp;
    float maxHp;

    // Start is called before the first frame update
    void Update()
    {
        imageFilled.fillAmount = Mathf.Lerp(imageFilled.fillAmount, hp / maxHp, Time.deltaTime * 5f);
    }

    public void OnInit(float maxHp)
    {
        this.maxHp = maxHp;
        hp = maxHp;
        imageFilled.fillAmount = 1;
    }


    public void SetHp(float hp)
    {
        this.hp = hp;
        //imageFilled.fillAmount = hp / maxHp;
    }

    public float GetHp()
    {
        return this.hp;
    }
}
