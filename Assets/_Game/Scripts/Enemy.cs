using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enermy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    
    private bool isRight = true;
    private IState currentState;
    private Character target;
    [SerializeField] private GameObject parentEnermy;
    [SerializeField] private GameObject attackArea;
    
    GameObject HealClone;
    GameObject DrugClone;
  


    public Character Target => target;
   
    public override void OnInit()
    {
        base.OnInit();
        ChangeState(new IdleState());
        DeActiveAttack();
    }
    
    public override void OnDespawn()
    {
        base.OnDespawn();
    }
    protected override void OnDeath()
    {
        ChangeState(null);
        base.OnDeath();
        Invoke(nameof(Des), 2f);
    }
    private void Des()
    {
        GameController1.Instance.EnermyClone = parentEnermy;
        GameController1.Instance.EnermyDead();
    }
    private void Update()
    {
        if (hp <= 0) 
        {
            rb.velocity = Vector3.zero;
        }
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }
    public void ChangeState(IState newState)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        if(currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    internal void SetTarget(Character character)
    {
        this.target = character;
        if(IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if(Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new IdleState());
        }
    }
    public void Moving()
    {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }
    public void StopMoving()
    {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }
    public void Attack()
    {
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    public bool IsTargetInRange()
    {
        if(target!=null&& Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else { return false; }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
        if(collision.tag =="DeathZone")
        {
            OnDeath();
        }
    }
    public void ChangeDirection(bool isRight)
    {
        
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
        
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

}
