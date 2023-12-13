using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{

    [SerializeField] private NavMeshAgent agent;
    public Vector3 destionation;

    public bool IsDestionation => Vector3.Distance(destionation, transform.position.x * Vector3.right + Vector3.up + Vector3.forward * transform.position.z) < 0.2f;
    public float distance => Vector3.Distance(destionation, transform.position.x * Vector3.right +Vector3.up + Vector3.forward * transform.position.z);
    // Start is called before the first frame update

    private void Start()
    {
        //changAnim("idle");
        destionation = transform.position;   
    }


    public void SetDestionation(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 1f;
        agent.SetDestination(destionation);

    }

    IState<Bot> currentState;
    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
            Debug.DrawRay(transform.position+ Vector3.forward + Vector3.up, Vector3.down, Color.red, lenghtRaycast);
            CanMove(transform.position + Vector3.forward + Vector3.up);
        }
    }
    public void changState(IState<Bot> state )
    {
        if(currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if(currentState != null )
        {
            currentState.OnEnter(this);
        }

    }

}
