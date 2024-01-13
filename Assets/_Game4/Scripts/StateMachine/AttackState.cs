using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        Debug.Log("Attack Enter");
        t.ChangeAnim(Anim.attackAnim);
    }

    public void OnExecute(Bot t)
    {
        Debug.Log("Attack Execute");
        if(!t.isAttack){
            t.agent.enabled = true;
            if(Random.Range(0,10) % 3 == 0){
                t.ChangeState(new PatrolState());
            }
            else{
                t.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Bot t)
    {
        Debug.Log("Attack Exit");
        // t.changeAnim("idle");
        // t.agent.enabled = true;
    }

}