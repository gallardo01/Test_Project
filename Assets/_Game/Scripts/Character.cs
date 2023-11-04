using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected Animator anim;
    [SerializeField] protected CombatText combatTextPrefab;

    private float hp;
    protected string currentAnimName = "idle";

    public bool IsDead => hp <= 0;

    private void Start() {
        OnInit();
    }

    public virtual void OnInit() {
        hp = 100;
        healthBar.OnInit(100, transform);
    }

    public virtual void OnDespawn() {
        
    }

    public void OnHit(float damage) {
        if (!IsDead) {
            hp -= damage;
            if (IsDead) {
                hp = 0;
                OnDeath();
            }

            healthBar.SetNewHp(hp);
            Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
        }
    }

    protected void PauseAnimator() {
        anim.speed = 0;
    }

    protected void PlayAnimator() {
        anim.speed = 1;
    }

    protected void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(currentAnimName);

            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        
        Invoke(nameof(OnDespawn), 2f);
    }

}
