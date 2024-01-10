using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        if (Vector3.Distance(bot.transform.position, randomPos) <= 0.0001f)
        {
            bot.changState(bot.idle);
        }
        else if ((bot.checkTarget() && bot.IsWeapon))
        {
            if(Random.Range(1,10) == 1)
            {
                bot.changState(bot.idle);
            }
        }
        
        

    }

    public void OnExit(Bot bot)
    {

    }

}
