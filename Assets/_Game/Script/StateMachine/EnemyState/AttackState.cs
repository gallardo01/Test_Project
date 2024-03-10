using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IState<Bot>
{
    private float resetAttack = 0.5f;
    private float timeCount;
    private bool isThrow;
    private int countAttack;
    public void OnEnter(Bot bot)
    {
        bot.SetDestionation(bot.transform.position);
        isThrow = false;
        timeCount = resetAttack;
        countAttack = 0;

    }

    public void OnExecute(Bot bot)
    {
        bot.count.Excute();
        timeCount += Time.deltaTime;
        if (bot.checkTarget())
        {
            Attack(bot);
        }


    }

    public void OnExit(Bot bot)
    {

    }
    private void Attack(Bot bot)
    {
        if (timeCount > resetAttack && !isThrow)
        {
            bot.IsWeapon = false;
            isThrow = true;
            bot.RotateTarget();
            if (bot.isUlti)
            {
                bot.ChangAnim(Constants.ANIM_ULTI);

            }
            else
            {
                bot.ChangAnim(Constants.ANIM_ATTACK);
            }
            bot.count.Start(Throw, 0.4f);
        }
         void Throw()
        {
            bot.Throw();
            bot.changState(bot.move);

        }
    }


}
