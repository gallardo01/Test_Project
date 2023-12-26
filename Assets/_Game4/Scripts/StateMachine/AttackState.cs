using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        Debug.Log("Attack Enter");
        t.changeAnim("attack");
        t.canMove = false;
        
    }

    public void OnExecute(Bot t)
    {
        Debug.Log("Attack Execute");
    }

    public void OnExit(Bot t)
    {
        // Debug.Log("Idle 3");
        // t.changeAnim("idle");
    }

}