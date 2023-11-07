using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{

    float timer;

    public void OnEnter(Enemy enemy)
    {
       if(enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x); // chuyen huong enemy ve phia target
            enemy.StopMoving();
            enemy.Attack();
        }
        timer = 0;
    }

    public void OnExcute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            enemy.ChangState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }

}
