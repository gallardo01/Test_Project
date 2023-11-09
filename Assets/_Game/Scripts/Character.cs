using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected float hp,maxHP;
    private string currentAnimName;
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] CombatText CombatTextPrb;
    [SerializeField] CombatText HealTextPrb;
    public bool isDead => hp <= 0f;
    private void Start()
    {
        OnInit();
    }
    
    public virtual void OnInit()
    {
        maxHP = 200;
        hp = maxHP;
        healthBar.OnInit(200,transform);
    }
    public virtual void OnDespawn()
    {
        
    }
    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);
    }
    public void OnHit(float damage)
    {
        if (!isDead)
        {
            
            hp -= damage;
            if (isDead)
            {
                hp = 0;
                OnDeath();
            }
            healthBar.SetNewHp(hp);
            Instantiate(CombatTextPrb, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }
    public void OnHeal(float HP)
    {
        if (!isDead)
        {

            if (hp < maxHP)
            {
                if (hp + HP > maxHP)
                {
                    HP = maxHP - hp;
                }
                hp += HP;
                Instantiate(HealTextPrb, transform.position + Vector3.up, Quaternion.identity).OnInit(HP);
                healthBar.SetNewHp(hp);
            }
        }
    }
    
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
