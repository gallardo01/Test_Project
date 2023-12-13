using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    int targetBrick;
    public void OnEnter(Bot t)
    {
        t.changAnim(Constants.ANIM_RUN);
        targetBrick = 6;
    }

    public void OnExecute(Bot t)
    {
       // Debug.Log("excute:" + t.distance);
        if (t.IsDestionation)
        {
            if(t.BrickCounts >= targetBrick)
            {
                t.changState(new AttackState());
            }
            else
            {
                seekTarget(t);
            }
        }
    }

    public void OnExit(Bot t)
    {

    }

    private void seekTarget(Bot t)
    {
        if(t.stage != null)
        {
            
            Brick brick = t.stage.seekBrickPoint(t.colorType);
            if(brick == null)
            {
                t.changState(new AttackState());
            }
            else
            {
                t.SetDestionation(brick.transform.position);
                //Debug.Log(t.de)
            }
        }
        else
        {
            t.SetDestionation(t.transform.position);
        }
    }
}
