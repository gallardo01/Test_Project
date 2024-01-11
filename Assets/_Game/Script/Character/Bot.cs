using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Bot : Character
{

    internal IdleState idle = new IdleState();
    internal MoveState move = new MoveState();
    internal AttackState attack = new AttackState();
    internal DeadState dead = new DeadState();

    [SerializeField] private NavMeshAgent agent;
    private Vector3 destionation;

    IState<Bot> currentState;


    public override void OnInit()
    {
        base.OnInit();
        attackRange = 5f;
        score = LevelManager.Instance.RandomPoint();
        deadScore = 1;
        currentScale = 1;
        this.agent.speed = 5f;
        this.GrowthCharacter();
        this.typeWeapon = LevelManager.Instance.RandomWeapon();
        this.ChangeWeaponImg();
        if (targetIndicator != null )
        {
            targetIndicator.setScore(score);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void changState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }

    }
    public void SetDestionation(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(destionation);

    }



    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
    }

    public override void GrowthCharacter()
    {
        base.GrowthCharacter();
        attackRange = 5f * currentScale;
        agent.speed = 5F * currentScale;
    }
}
