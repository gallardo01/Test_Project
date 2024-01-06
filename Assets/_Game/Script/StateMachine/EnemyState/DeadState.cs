using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : IState<Bot>
{

    public void OnEnter(Bot bot)
    {
       
        bot.SetDestionation(bot.transform.position);
        EasyObjectPool.instance.ReturnObjectToPool(bot.targetIndicator.gameObject);
        bot.ChangAnim(Constants.ANIM_DIE);
        bot.count.Start(Dead, 0.8f);
         void Dead()
        {
            EasyObjectPool.instance.ReturnObjectToPool(bot.gameObject);
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
