using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveState : IState<Bot>
{
    private Vector3 randomPos;
    private int randAttack;
    public void OnEnter(Bot bot)
    {
        randomPos = LevelManager.Instance.GetRandomPointOnNavMesh();
        randAttack = Random.Range(0, 2);
    }

    public void OnExecute(Bot bot)
    {
        bot.SetDestionation(randomPos);
        bot.ChangAnim(Constants.ANIM_RUN);
        if (Vector3.Distance(bot.transform.position, randomPos) <= 0.0001f || (bot.checkTarget() && bot.IsWeapon && randAttack == 0))
        {
            bot.changState(bot.idle);
        }
        
        
        

    }

    public void OnExit(Bot bot)
    {

    }

}
