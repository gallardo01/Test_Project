using ChangeAnim;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IState<Bot>
{
   // int targetBrick;
    //Patrol di nhat gach
    public void OnEnter(Bot t)
    {
        //targetBrick = 5;
    }

    public void OnExecute(Bot t)
    {
        t.changeAnim(Constants.ANIM_RUN);
        SeekTarget(t);
    }   
    public void OnExit(Bot t)
    {

    }
    private void SeekTarget(Bot t)
    {
        float x = Random.Range(-49f, 49f);
        float z = Random.Range(-49f, 49f);
        t.SetDestination(new Vector3(x, 0f, z));
    }
}
