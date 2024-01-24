using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        // Debug.Log("Idle Enter");
        t.ChangeAnim(Anim.idleAnim);
        t.Counter.Start(() => t.ChangeState(new PatrolState()), Random.Range(1f,5f));
    }

    public void OnExecute(Bot t)
    {
        // Debug.Log("Idle Execute");
        t.Counter.Execute();
    }

    public void OnExit(Bot t)
    {
        // Debug.Log("Idle Exit");
    }

}