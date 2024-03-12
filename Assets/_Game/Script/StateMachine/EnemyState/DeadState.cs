using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : IState<Bot>
{

    public void OnEnter(Bot bot)
    {
       
        bot.SetDestionation(bot.TF.position);
        bot.targetIndicator.OnDespawn();
        bot.ChangAnim(Constants.ANIM_DIE);
        bot.count.Start(Dead, 0.8f);
         void Dead()
        {
            SimplePool.Despawn(bot);
        }



    }


    public void OnExecute(Bot bot)
    {
        bot.count.Excute();
    }

    public void OnExit(Bot bot)
    {
        
    }

 
}
