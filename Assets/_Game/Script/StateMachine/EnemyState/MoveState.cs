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
        //randAttack = Random.Range(0, 2);
        bot.SetDestionation(randomPos);
        bot.ChangAnim(Constants.ANIM_RUN);
    }

    public void OnExecute(Bot bot)
    {
        
        
        if (Vector3.Distance(bot.TF.position, randomPos) <= 0.0001f || (bot.checkTarget() && bot.IsWeapon))
        {
            bot.changState(bot.idle);
        }
        
        
        

    }

    public void OnExit(Bot bot)
    {

    }

}
