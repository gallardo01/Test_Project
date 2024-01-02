using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Player
{

    public NavMeshAgent agent;

    private Vector3 destination;
    private IState<Bot> currentState;
    private CounterTime counter;
    private Transform character;


    public CounterTime Counter => counter;
    public bool IsDestination => Vector3.Distance(transform.position, destination) < 0.34f;
    public Transform Character { set => character = value; }

    private void Start()
    {
        counter = new CounterTime();
        ChangeState(new PatrolState());
        canAttack = false;
        Invoke(nameof(EnableAttack), 10);
    }

    private void EnableAttack()
    {
        canAttack = true;
    }

    private void Update()
    {
        if (currentState != null) currentState.OnExecute(this);
        ToCallInUpdate();
        if (Mathf.Abs(transform.position.x) > 49 || Mathf.Abs(transform.position.z) > 49) agent.ResetPath();
    }

    public void SetDestination(Vector3 position)
    {
        destination = position;
        agent.SetDestination(destination);

        transform.LookAt(destination);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        BotPool.Release(this);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        ChangeState(null);
        agent.ResetPath();
    }

    public void ChangeState(IState<Bot> state)
    {
        if (currentState != null) currentState.OnExit(this);

        currentState = state;

        if (currentState != null) currentState.OnEnter(this);
    }

    public void ResetPath()
    {
        agent.ResetPath();
    }

}
