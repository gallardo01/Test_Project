using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MarchingBytes;

public class Bot : Character
{
    public NavMeshAgent agent;
    private Vector3 destination;

    public bool IsDestination => (Mathf.Abs(destination.x - transform.position.x) + Mathf.Abs(destination.z - transform.position.z)) < 0.05f;
    // public int targetBrick;
    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;
    // public float distanceY;    
    // Start is called before the first frame update
    void Start()
    {
        // destination = transform.position;
        // changeAnim("idle");
        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    void UpDate()
    {
        // transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        OnAttack();
    }

    public void SetDestination(Vector3 position)
    {  
        agent.enabled = true;
        destination = position;
        destination.y = 0.5f;
        agent.SetDestination(position);
        ChangeAnim(AnimConstant.runAnim);
        // Debug.Log("Destination: "+position);
    }

    public override void Attack(Vector3 point){
        base.Attack(point);
        SetDestination(transform.position);
        agent.enabled = false;
        ChangeState(new AttackState());
    }

    IState<Bot> currentState;
    private void Update(){
        if(currentState != null){
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Bot> state){
        if(currentState != null){
            currentState.OnExit(this);
        }
        currentState = state;
        if(currentState != null){
            currentState.OnEnter(this);
        }
    }

    // public void AddTarget(Character target)
    // {
    //     // base.AddTarget(target);
    //     if (Random.Range(0, 2) == 0 && Camera.main.WorldToViewportPoint(transform.position).x < 1f && Camera.main.WorldToViewportPoint(transform.position).y < 1f)
    //     {
    //         ChangeState(new AttackState());
    //         Invoke(nameof(ChangeStateAfterAttack), 1f);
    //     }
    // }
}