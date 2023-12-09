using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    // Attack -> Di ve dich

    public void OnEnter(Bot t)
    {
        Debug.Log("Attack Enter");
        t.SetDestination(LevelManager.Ins.FinishPoint);
    }

    // Dieu kien de ko di ve dich nua, chuyen trang thai
    public void OnExecute(Bot t)
    {
        Debug.Log("Attack Execute");

        if (t.BrickCount == 0)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Bot t)
    {

    }

}
