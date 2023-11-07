using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offSet;
    float hp,maxHp;
    // Update is called once per frame
    private Transform Target;
    void Update()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp/maxHp,Time.deltaTime * 5f);
        transform.position = Target.position + offSet;
    }
    public void OnInit(float maxHp,Transform target)
    {
        this.Target = target;
        this.maxHp = maxHp;
        hp = maxHp;
        imageFill.fillAmount = 1;
    }
    public void SetNewHp(float hp)
    {
        this.hp = hp;
    }
}
