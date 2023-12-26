using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState : IState<Bot>
{
    private float resetAttack = 0.5f;
    private float timeCount;
    private float timeChange = 1f;
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
        
        if (bot.targets.Count <= 0 || timeCount > 2f)
        {
            bot.changState(new PatrolState());
        }
        Character target = bot.GetTarget();
        if (target != null)
        {
            Attack(bot);
         
        }


    }

    public void OnExit(Bot bot)
    {

    }
    private void Attack(Bot bot)
    {
        if(timeCount > resetAttack && !isThrow && bot.target.GetComponent<Character>().collider == true)
        {
            isThrow = true;
            bot.RotateTarget();
            bot.ChangAnim(Constants.ANIM_ATTACK);
            bot.OnAttack();
        }
        
        
    }

}
