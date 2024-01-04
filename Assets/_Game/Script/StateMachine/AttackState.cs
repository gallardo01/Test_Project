using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    // Attack -> Di ve dich

    public void OnEnter(Bot t)
    {
        t.OnMoveStop();
        t.OnAttack();
    }

    public void OnExecute(Bot t)
    {
    }

    public void OnExit(Bot t)
    {

    }

}
