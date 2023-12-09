using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        Debug.Log("Attack Enter");
        // t.changeAnim("run");

        t.SetDestination(LevelManager.Ins.FinishPoint);
    }

    public void OnExecute(Bot t)
    {
        if(t.BrickCount == 0){
            Debug.Log("Attack Execute");
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {

    }

}
