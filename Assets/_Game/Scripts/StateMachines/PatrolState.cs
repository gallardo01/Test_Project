using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState<Bot>
{

    private float moveRange = 6f;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim("IsRun");
        Vector3 destination;
        NavMeshPath navMeshPath = new NavMeshPath();
        do
        {
            destination = t.transform.position + new Vector3(Random.Range(-50, 50), 0, Random.Range(-50, 50)).normalized * moveRange;;
        } while (!(t.agent.CalculatePath(destination, navMeshPath) && navMeshPath.status == NavMeshPathStatus.PathComplete));
        t.SetDestination(destination);
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
