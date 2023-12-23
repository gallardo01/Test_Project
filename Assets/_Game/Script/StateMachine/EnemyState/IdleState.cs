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
        bot.count.Start(() => bot.changState(new PatrolState()), Random.Range(1f, 3f));
    }

    public void OnExecute(Bot bot)
    {

        bot.count.Excute();
        if (bot.targets.Count>0)
        {
            bot.count.Cancel();
            bot.changState(new AttackState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

}
