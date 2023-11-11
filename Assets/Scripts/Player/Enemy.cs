using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float range;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private GameObject healtpot;

    private iState currentState;

    private float randomNum;
    private bool isRight = true;

    private Character target;
    public Character Target => target;

    private void Update()
    {
        if (currentState != null && !IsDead)
        {
            currentState.OnExecute(this);
        }
    }
    public override void OnInit() 
    {
        base.OnInit();
        ChangeState(new Idle());
        InActiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        randomNum = Random.Range(0, 2);
        if (randomNum == 1)
        {
            Instantiate(healtpot, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        Changeanim("Die");
        ChangeState(null);
        base.OnDeath();
        GameController.Instance.decreaseEnemyCount();
    }

    public void ChangeState(iState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    internal void SetTarget(Character character)
    {
        this.target = character;

        //doan nay sai logic khi no set target = null no se bug null neu k check
        if(Target != null && InRange())
        {
            ChangeState(new AttackState());
        }
        else if (Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new Idle());
        }

    }
    public void Moving()
    {
        Changeanim("Run");
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving()
    {
        Changeanim("Idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack()
    {
        Changeanim("Attack");
        ActiveAttack();
        Invoke(nameof(InActiveAttack), 0.5f);
    }

    public bool InRange()
    {
        return Vector2.Distance(target.transform.position, transform.position) <= range;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DirectionPoint"))
        {
            ChangeDirection(!isRight);
        }

        if (collision.CompareTag("Deathzone"))
        {
            OnDeath();
        }   
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void InActiveAttack()
    {
        attackArea.SetActive(false);
    }
}
