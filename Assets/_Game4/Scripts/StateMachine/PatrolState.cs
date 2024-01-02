using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        Debug.Log("Patrol Enter");

        t.ChangeAnim("run");
        SeekTarget(t);
        // t.targetBrick = 5;
    }

    public void OnExecute(Bot t)
    {
        if(t.IsDestination){
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot t)
    {

    }

    private void SeekTarget(Bot t){
        t.SetDestination(new Vector3(Random.Range(-24f,24f),0,Random.Range(-24f,24f)));
    }

}   