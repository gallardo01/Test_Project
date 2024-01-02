using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.ChangeAnim("idle");
        // t.ChangeState(new PatrolState());
        t.Counter.Start(() => t.ChangeState(new PatrolState()), Random.Range(1f,5f));
        // Invoke(nameof(ChangePatrolState), 1f);
    }

    // private void ChangePatrolState(){
    // }

    public void OnExecute(Bot t)
    {
        t.Counter.Execute();
    }

    public void OnExit(Bot t)
    {

    }

}