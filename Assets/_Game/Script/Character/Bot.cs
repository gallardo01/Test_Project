using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Bot : Character
{

    [SerializeField] private NavMeshAgent agent;
    public Vector3 destionation;

    private bool IsDestionation => Mathf.Abs((destionation.x - transform.position.x)+ (destionation.z - transform.position.z)) <=  0.01f;
    IState<Bot> currentState;
    // Start is called before the first frame update
    void Start()
    {
        ChangAnim(Constants.ANIM_IDLE);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }
    public void changState(IState<Bot> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = state;
        if (currentState != null)
        {
            currentState.OnEnter(this);
        }

    }
    public void SetDestionation(Vector3 position)
    {
        agent.enabled = true;
        destionation = position;
        destionation.y = 0;
        agent.SetDestination(destionation);

    }
    public override void OnAttack()
    {
        base.OnAttack();
        count.Start(ThrowWeapon, 0.4f);
        //ChangAnim(Constants.ANIM_IDLE);

    }
}
