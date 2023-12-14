using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    //Attack Di ve dich
    public void OnEnter(Bot t)
    {
        t.SetDestination(LevelManager.Instance.FinishPoint);
    }
    //Dk ko di ve dich
    public void OnExecute(Bot t)
    {
        if(t.BrickCount == 0)
        {
            Debug.Log("True");
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {

    }

}
