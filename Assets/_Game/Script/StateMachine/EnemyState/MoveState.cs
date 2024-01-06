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
        if(Vector3.Distance(bot.transform.position , randomPos) <= 0.0001f || (bot.checkTarget() && bot.IsWeapon))
        {
            bot.changState(bot.idle);
            
        }
        //else if((bot.checkTarget() && bot.IsWeapon))
        //{
        //    //if (Camera.main != null)
        //    //{
        //    //    if (Random.Range(0, 2) == 0 && Camera.main.WorldToViewportPoint(bot.transform.position).x < 1f && Camera.main.WorldToViewportPoint(bot.transform.position).y < 1f)
        //    //    {
        //    //        bot.changState(bot.idle);
        //    //    }
        //    //}
           
        //}
        

    }

    public void OnExit(Bot bot)
    {

    }

}
