using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] protected Animator anim;
    [SerializeField] protected CombatText combatTextPrefab;

    protected float hp;
    private String previousAnimName = "idle";
    protected string currentAnimName = "idle";

    public bool IsDead => hp <= 0;

    private void Start() {
        OnInit();
    }

    protected void ToPreviousAnim() {
        ChangeAnim(previousAnimName);
    }

    public virtual void OnInit() {
        hp = 100;
        healthBar.OnInit(100);
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
            OnHealthChanged();
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

            previousAnimName = currentAnimName;
            currentAnimName = animName;

            anim.SetTrigger(currentAnimName);
        }
    }

    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        Invoke(nameof(OnDespawn), 2f);
    }

    public void Heal(int amount) {
        hp += amount;
        OnHealthChanged();
    }

    private void OnHealthChanged() {
        healthBar.SetNewHp(hp);
        if (gameObject.tag == "Player") anim.SetFloat("speed", 1f + (100 - hp) / 100);
    }
}
