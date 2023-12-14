using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        Debug.Log("Patrol Enter");

        t.changeAnim("run");
        t.targetBrick = 5;
    }

    public void OnExecute(Bot t)
    {
        if(!t.IsDestination){
            if(t.BrickCount >= t.targetBrick){
                t.ChangeState(new AttackState());
            }
            else{
                SeekTarget(t);
            }
        }
        else{
            Debug.Log("Idle 1" + t.colorType);
            t.changeAnim("idle");
        }
    }

    public void OnExit(Bot t)
    {
        // Debug.Log("Idle 2");
        // t.changeAnim("idle");
    }

    private void SeekTarget(Bot t){
        if(t.stage != null){
            Brick brick = t.stage.SeekBrickPoint(t.colorType);

            if(brick == null){
                t.ChangeState(new AttackState());
            }
            else{
                t.SetDestination(brick.transform.position);
            }
        }
        else{
            t.SetDestination(t.transform.position);
        }
    }

}