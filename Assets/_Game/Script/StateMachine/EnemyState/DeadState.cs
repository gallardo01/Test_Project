using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeadState : IState<Bot>
{

    private float timeDead = 0.7f;
    private float timeCount;
    public void OnEnter(Bot bot)
    {
        bot.collider.enabled = false;
        timeCount = 0;
        bot.SetDestionation(bot.transform.position);
    }

    public void OnExecute(Bot bot)
    {
        bot.ChangAnim(Constants.ANIM_DIE);
        timeCount += Time.deltaTime;
        if(Mathf.Abs(timeDead - timeCount) < 0.01f)
        {

            OnExit(bot);
        }
    }

    public void OnExit(Bot bot)
    {
        Object.Destroy(bot.gameObject);
    }
}
