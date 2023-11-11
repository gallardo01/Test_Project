using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : iState
{
    //private Enemy enemy;
    //private float range;
    //private float moveSpeed;
    float randomTime;
    float timer;

    //public PatrolState(Enemy enemy, float range, float moveSpeed)
    //{
    //    this.enemy = enemy;
    //    this.range = range;
    //    this.moveSpeed = moveSpeed;
    //}

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(1f, 3f);
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;

        if (enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            if (enemy.InRange())
            {
                enemy.ChangeState(new AttackState());
            }
            else
            {
                enemy.Moving();
            }
        }

        else
        {
            if (timer < randomTime)
            {
                enemy.Moving();
            }
            else
            {
                enemy.ChangeState(new Idle());
            }
        }
    }

    public void OnExit(Enemy enemy)
    {

    }   
}
