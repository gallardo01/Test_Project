using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    int targetBrick;
    //Patrol di nhat gach
    public void OnEnter(Bot t)
    {
        t.changeAnim("run");
        targetBrick = 5;
    }

    public void OnExecute(Bot t)
    {
        if (t.BrickCount >= targetBrick)
        {
            Debug.Log(t.BrickCount);
            t.ChangeState(new AttackState());
        }
        else
        {
            SeekTarget(t);
        }
        //if (t.IsDestination)
        //{
            
        //}
    }

   
    private void SeekTarget(Bot t)
    {
        if (t.stage != null)
        {
            Brick brick = t.stage.SeekBrickPoint(t.colorType);
            if(brick == null)
            {
                t.ChangeState(new AttackState());
            }
            else
            {
                t.SetDestination(brick.transform.position);
            }
        }
        else
        {
            t.SetDestination(t.transform.position);
        }
    }
    public void OnExit(Bot t)
    {

    }
}
