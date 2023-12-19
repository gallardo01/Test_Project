using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IdleState : IState<Bot>
{
    public float timeIdle;
    public void OnEnter(Bot bot)
    {
        timeIdle = 0;
        bot.SetDestionation(bot.transform.position);
        bot.ChangAnim(Constants.ANIM_IDLE);
    }

    public void OnExecute(Bot bot)
    {
        timeIdle += Time.deltaTime;
        if((timeIdle - 2)> 0.0001f)
        {
            bot.changState(new PatrolState());
        }
    }

    public void OnExit(Bot bot)
    {

    }

}
