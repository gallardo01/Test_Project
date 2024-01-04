using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destionation;

    public bool IsDestintion => (Mathf.Abs(destionation.x - transform.position.x) + Mathf.Abs(destionation.z - transform.position.z)) < 0.05f;

    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;

    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        ChangeState(new PatrolState());
        //changeAnim("idle");
    }

    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0.5f;
        agent.SetDestination(position);
    }

    IState<Bot> currentState;
    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
        counter.Execute();
    }

    public override void OnInit()
    {
        base.OnInit();
    }
    public void ChangeState(IState<Bot> state)
    {
        if (!isDead)
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
    }

    public override void OnDeath()
    {
        isDead = true;
        targetIndicator.gameObject.SetActive(false);
        // quay ve main menu
        base.OnDeath();
        ChangeState(null);
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;    
        Invoke(nameof(disableSelf), 3f);
    }

    private void disableSelf()
    {
        gameObject.SetActive(false);
    }

    public override void OnAttack()
    {
        base.OnAttack();
        target = GetTargetInRange();
        changeAnim("attack");
        counter.Start(Throw, 0.5f);
    }

    public void OnMoveStop()
    {
        agent.enabled = false;
        changeAnim("idle");
    }

    public override void AddTarget(Character target)
    {
        base.AddTarget(target);
        if (Random.Range(0, 2) == 0 && Camera.main.WorldToViewportPoint(transform.position).x < 1f && Camera.main.WorldToViewportPoint(transform.position).y < 1f)
        {
            ChangeState(new AttackState());
            Invoke(nameof(ChangeStateAfterAttack), 1f);
        }
    }

    private void ChangeStateAfterAttack()
    {
        if (!isDead)
        {
            if (Random.Range(0, 2) == 0)
            {
                ChangeState(new IdleState());
            }
            else
            {
                ChangeState(new PatrolState());
            }
        }
    }
}
