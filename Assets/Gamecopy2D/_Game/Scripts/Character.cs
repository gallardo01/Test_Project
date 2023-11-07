using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HeathBar heathBar;
    [SerializeField] protected CombatText combatText;


    private float hp;
    private string currentAnimName;

    public bool IsDeath => hp <= 0;

    private void Start()
    {
        OnInit();
    }
    // ham tao
    public virtual void OnInit()
    {
        hp = 100;
        heathBar.OnInit(100, transform);
    }
    // ham huy

    public virtual void OnDespawn()
    {
        
    }

    protected virtual void OnDead()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 1.5f);
    }



    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)

        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public void OnHit(float damage)
    {
        if (!IsDeath)
        {
            hp -= damage;
            if (IsDeath)
            {
                OnDead();
                hp = 0;
            }
            heathBar.setnewHp(hp);
            Instantiate(combatText, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

}
