using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    int targetBrick;
    public void OnEnter(Bot t)
    {
        Debug.Log("Patrol Enter");

        t.changeAnim("run");
        targetBrick = 5;
    }

    public void OnExecute(Bot t)
    {
        if(!t.IsDestination){
            if(t.BrickCount >= targetBrick){
                t.ChangeState(new AttackState());
            }
            else{
                SeekTarget(t);
            }
        }
    }

    public void OnExit(Bot t)
    {

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
