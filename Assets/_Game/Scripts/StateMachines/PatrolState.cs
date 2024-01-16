using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState<Bot>
{

    public void OnEnter(Bot t)
    {
        t.ChangeAnim("IsRun");
        Vector3 destination;
        NavMeshHit hit;
        int count = 0;
        do {
            Vector3 direction = Random.insideUnitSphere;
            destination = t.transform.position +  (direction - Vector3.up * direction.y) * t.moveRange;
            if (NavMesh.SamplePosition(destination, out hit, 1.0f, NavMesh.AllAreas)) {
                t.SetDestination(destination);
                break;
            }
            count++;
        } while (count < 30);

        if (count == 30) t.ChangeState(new IdleState());
    }

    public void OnExecute(Bot t)
    {
        if (t.IsDestination)
        {
            t.ResetPath();
            t.ChangeState(new IdleState());
        }
    }

    public void OnExit(Bot t)
    {
    }

}
