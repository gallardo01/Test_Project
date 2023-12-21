using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        // wait - run animation
        // dung lai 1-5s
        t.changeAnim("idle");
        t.ChangeState(new PatrolState());
    }

    public void OnExecute(Bot t)
    {

    }

    public void OnExit(Bot t)
    {

    }

}
