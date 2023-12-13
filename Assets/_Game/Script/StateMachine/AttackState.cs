using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        
        t.SetDestionation(LevelManager.Ins.FinishPoint);
    }

    public void OnExecute(Bot t)
    {
        

        if(t.BrickCounts == 0)
        {
            if(t.CanMove(t.transform.position + Vector3.forward + Vector3.up))
            {
                t.SetDestionation(LevelManager.Ins.FinishPoint);
            }
            else
            {
                t.SetDestionation(t.transform.position);
                t.changState(new PatrolState());
            }
        }
    }

    public void OnExit(Bot t)
    {

    }

}
