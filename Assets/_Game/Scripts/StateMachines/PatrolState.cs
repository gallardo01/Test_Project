using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState<Bot>
{

    private Vector3 previousPosition;
    private Vector3 direction;

    private PatrolState(Vector3 direction)
    {
        this.direction = direction;
    }

    public PatrolState()
    {
        direction = -Vector3.one;
    }

    public void OnEnter(Bot t)
    {
        t.ChangeAnim("IsRun");
        Vector3 destination;
        if (direction == -Vector3.one)
        {
            direction = Vector3.one * .1f + Random.insideUnitSphere;
            direction -= Vector3.up * direction.y;
        }
        destination = t.transform.position + direction * t.moveRange;
        t.SetDestination(destination);
        previousPosition = destination;
    }

    public void OnExecute(Bot t)
    {
        if (t.IsDestination)
        {
            t.ResetPath();
            t.ChangeState(new IdleState());
        } else 

        // renew destination if staying in the same position for 2 consecutive frames
        if (previousPosition == t.transform.position)
        {
            t.ResetPath();
            t.ChangeState(new PatrolState(-direction));
        }

        previousPosition = t.transform.position;
    }

    public void OnExit(Bot t)
    {
    }

}
