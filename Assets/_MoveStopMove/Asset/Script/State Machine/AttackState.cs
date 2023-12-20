using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    //Attack Di ve dich
    public void OnEnter(Bot t)
    {
     
    }
    //Dk ko di ve dich
    public void OnExecute(Bot t)
    {

       // t.ChangeState(new PatrolState());
    }

    public void OnExit(Bot t)
    {

    }
}