using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IState<Bot>
{
    private float resetAttack = 2f;
    private float timeCount;
    public void OnEnter(Bot bot)
    {
        bot.SetDestionation(bot.transform.position);
        timeCount = resetAttack;
        

    }

    public void OnExecute(Bot bot)
    {
        timeCount += Time.deltaTime;
        if(bot.targets.Count <= 0)
        {
            bot.changState(new PatrolState());
        }
        if(timeCount >= resetAttack)
        {
            Character target = bot.GetTarget();
            if(target != null)
            {
                Attack(bot);
            }
            else
            {
                bot.changState(new PatrolState());
            }
        }
        
        
    }

    public void OnExit(Bot bot)
    {

    }
    private void Attack(Bot bot)
    {
        timeCount = 0;
        bot.ChangAnim(Constants.ANIM_ATTACK);
        bot.ThrowWeapon();
        
    }

}
