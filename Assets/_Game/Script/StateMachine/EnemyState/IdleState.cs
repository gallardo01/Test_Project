using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public void OnEnter(Bot bot)
    {

        bot.SetDestionation(bot.transform.position);
        bot.ChangAnim(Constants.ANIM_IDLE);
        bot.count.Start(() => bot.changState(bot.move), Random.Range(1f, 3f));
    }

    public void OnExecute(Bot bot)
    {

        bot.count.Excute();
        if (bot.checkTarget() && bot.IsWeapon)
        {
            bot.count.Cancel();
            bot.changState(bot.attack);
        }
    }

    public void OnExit(Bot bot)
    {

    }

}
