using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AttackState : IState
{
    float timer;

    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null) {
            // doi huong enemy toi huong cua player
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            
            enemy.StopMoving();
            enemy.Attack();
        }

        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (timer >= 1.5f) {
            enemy.ChangeState(new PatrolState());
        } else if (timer >= 0.5) {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy)
    {
    }

}
