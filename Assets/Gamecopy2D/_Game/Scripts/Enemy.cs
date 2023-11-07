using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    [SerializeField] private float attackRange;
    [SerializeField] private float movingSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;


    private IState currentState;
    private bool IsRight = true;

    private Character targer; 
    public Character Target => targer;


    private void Update()
    {
        if(currentState != null && !IsDeath)
        {
            currentState.OnExcute(this);
        }
    }
    public override void OnInit()
    {
        base.OnInit();
        ChangState(new IdleState());
        DeActiveAttack();
    }

    protected override void OnDead()
    {
        ChangState(null);
        base.OnDead();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(heathBar.gameObject);
        Destroy(gameObject);
    }



    public void ChangState(IState Newstate)
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = Newstate;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }

    } // doi hanh dong cua enemy
    public void Moving()
    {
        ChangeAnim("run");

        rb.velocity = transform.right * movingSpeed;
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
    public bool IsTargetinRange() 
    {
        if(targer != null && Vector2.Distance(targer.transform.position, transform.position) < attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    } // kiem tra target co trong vung tan cong cua enemy k

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyWall")
        {
            ChangeDirection(!IsRight);
        }
    } // neu enemy cham vao wall thi doi huong

    public void ChangeDirection(bool isRight)
    {
        this.IsRight = isRight;
        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    } // doi huong cua enemy

    internal void SetTarget(Character character)
    {
        this.targer = character;
        if (IsTargetinRange())
        {
            ChangState(new AttackState());
        }
        else
        {
            if (targer != null)
            {
                ChangState(new PatrolState());
            }
            else
                ChangState(new IdleState());
        }
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
