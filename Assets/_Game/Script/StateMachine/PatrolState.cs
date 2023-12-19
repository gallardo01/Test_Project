using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolState : IState<Bot>
{
    int targetBrick;
    public void OnEnter(Bot t)
    {
        t.changeAnim("run");
    }

    public void OnExecute(Bot t)
    {
        SeekTarget(t);
    }

    public void OnExit(Bot t)
    {

    }

    private void SeekTarget(Bot t)
    {
        t.SetDestination(new Vector3(Random.Range(-15f, 15f),0.5f, Random.Range(-15f, 15f)));
    }
}
