using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    
    private IState currentState;
    private bool isRight = true;
    private Character target;

    public Character Target => target;

    private void Update() {
        if (currentState != null) {
            currentState.OnExecute(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "EnemyWall") {
            ChangeDirection(!isRight);
        }
    }

    private void ActiveAttack() {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack() {
        attackArea.SetActive(false);
    }

    protected override void OnDeath()
    {
        ChangeState(null);

        base.OnDeath();
    }

    internal void SetTarget(Character character)
    {
        this.target = character;

        if (IsTargetInRange()) {
            ChangeState(new AttackState());
        } else if (target != null) {
            ChangeState(new PatrolState());
        } else {
            ChangeState(new IdleState());
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new IdleState());

        DeActiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        Destroy(healthBar.gameObject);

        Destroy(gameObject);
    }
    
    public void ChangeState(IState newState) {
        if (currentState != null) {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null) {
            currentState.OnEnter(this);
        }
    }

    public void Moving() {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving() {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack() {
        ChangeAnim("attack");

        ActiveAttack();

        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public bool IsTargetInRange() {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange) {
            return true;
        } else {
            return false;
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    
}
