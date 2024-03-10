using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Bot : Character
{

    internal IdleState idle = new IdleState();
    internal MoveState move = new MoveState();
    internal AttackState attack = new AttackState();
    internal DeadState dead = new DeadState();

    [SerializeField] private NavMeshAgent agent;
    private Vector3 destionation;

    [SerializeField] public bool IsDestintion;

    public IState<Bot> currentState;


    public override void OnInit()
    {
        base.OnInit();
        this.target = null;
        this.count.Cancel();
        score = LevelManager.Instance.RandomPoint();
        deadScore = 1;
        currentScale = 1;
        this.GrowthCharacter();
        this.typeWeapon = LevelManager.Instance.RandomWeapon();
        this.ChangeWeaponImg();
        if (targetIndicator != null )
        {
            targetIndicator.setScore(score);
        }
        this.ChangAnim(Constants.ANIM_IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        if ((Mathf.Abs(destionation.x - transform.position.x) + Mathf.Abs(destionation.z - transform.position.z)) < 0.05f)
        {
            IsDestintion = true;
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

    public override bool checkTarget()
    {
        bool pos =  Camera.main.WorldToViewportPoint(this.transform.position).x < 1f && Camera.main.WorldToViewportPoint(this.transform.position).y < 1f;
        return base.checkTarget()  && pos;
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        this.collider.enabled = false;
        this.changState(dead);

    }

    public override void GrowthCharacter()
    {
        base.GrowthCharacter();
        attackRange = 5f * currentScale;
        agent.speed = 5f * currentScale;
    }

}
