using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Player
{
    public float moveRange = 10f;
    public NavMeshAgent agent;

    private Vector3 destination;
    private IState<Bot> currentState;
    private Transform character;
    private GameObject arrow;
    private Pool<Bot> pool;

    public CounterTime Counter => counter;
    public bool IsDestination => Vector3.Distance(transform.position, destination) < 0.34f;
    public Transform Character { set => character = value; }
    public GameObject Arrow { set => arrow = value; }
    public Pool<Bot> Pool { set => pool = value; }

    protected override void OnInit() {
        base.OnInit();
        canAttack = false;
        ChangeState(new PatrolState());

        // Can be duplicate in less than 5 seconds if player pause and restart immediately -> release all bot when game ends
        Invoke(nameof(EnableAttack), 5);
    }

    private void OnDisable() {
        CancelInvoke();
    }

    private void EnableAttack()
    {
        canAttack = true;
    }

    private void Update()
    {
        if (currentState != null) currentState.OnExecute(this);

        // Only attack when on screen
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x <= 1 && viewportPosition.x >= 0 && viewportPosition.y <= 1 && viewportPosition.y >= 0)
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
        pool.Release(this);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        ChangeState(null);
        agent.ResetPath();

        // Pool later
        Destroy(arrow);
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
