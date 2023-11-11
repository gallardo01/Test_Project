using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected Healthbar healthbar;
    [SerializeField] private CombatText combatTextPrefab;

    private float hp;
    private string currentAnim;

    public bool IsDead => hp <= 0;

    private void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        hp = 100;
        healthbar.OnInit(hp);
    }



    public virtual void OnDespawn()
    {
        
    }

    protected virtual void OnDeath()
    {
        Changeanim("Die");
        Invoke(nameof(OnDespawn), 1f);
    }

    public void OnHit(float damage)
    {
        if (!IsDead)
        {
            hp -= damage;
            if (IsDead)
            {
                hp = 0;
                OnDeath();
            }
            healthbar.SetHp(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    public void Heal(float amount)
    {
        hp += amount;
        healthbar.SetHp(hp);
    }

    protected void Changeanim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(animName);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
