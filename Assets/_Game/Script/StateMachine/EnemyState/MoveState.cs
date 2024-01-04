using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState<Bot>
{
    private Vector3 randomPos;
    public void OnEnter(Bot bot)
    {
        randomPos = LevelManager.Instance.GetRandomPointOnNavMesh();
    }

    public void OnExecute(Bot bot)
    {
        bot.SetDestionation(randomPos);
        bot.ChangAnim(Constants.ANIM_RUN);
        if(Vector3.Distance(bot.transform.position , randomPos) <= 0.0001f || (bot.checkTarget() && bot.IsWeapon))
        {
            bot.changState(bot.idle);
            
        }
        

    }

    public void OnExit(Bot bot)
    {

    }

}
