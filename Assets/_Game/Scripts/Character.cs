using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] protected HealthBar healthBar;
    [SerializeField] private CombatText combatTextPrefab;


    public float hp;
    protected bool isImmortal = false;
    private string currentAnimName;

    public bool IsDead => hp <= 0;

    private void Start(){
        OnInit();
    }

    public virtual void OnInit(){
        hp = 100;
        healthBar.OnInit(100, transform);
    }

    public virtual void OnDespawn(){

    }

    public void OnHit(float damage){
        if(!isImmortal){
            if(!IsDead){
                hp -= damage;

                if(IsDead){
                    hp = 0;
                    OnDeath();
                }

                healthBar.SetNewHp(hp);
                Instantiate(combatTextPrefab, transform.position + Vector3.up, Quaternion.identity).OnInit(damage);
            }   
        }
    }

    public void updateHealth(){
        healthBar.SetNewHp(hp);
    }

    protected void ChangeAnim(string animName){
        if(currentAnimName != animName){
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected virtual void OnDeath(){
        ChangeAnim("die");

        Invoke(nameof(OnDespawn), 2f);
    }

}
