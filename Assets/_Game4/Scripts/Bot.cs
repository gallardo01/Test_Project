using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using MarchingBytes;

public class Bot : Character
{
    public NavMeshAgent agent;
    public Vector3 destination;

    public bool IsDestination => (Mathf.Abs(destination.x - transform.position.x) + Mathf.Abs(destination.z - transform.position.z)) < 0.05f;
    // public int targetBrick;
    private CounterTime counter = new CounterTime();
    public CounterTime Counter => counter;
    // public float distanceY;    
    // Start is called before the first frame update
    void Start()
    {
        // destination = transform.position;
        ChangeAnim(Anim.idleAnim);
        ChangeState(new PatrolState());
        OnInit();
    }

    // Update is called once per frame
    // void UpDate()
    // {
    //     Debug.Log("Update");
    //     // transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    //     OnAttack();
    // }

    public override void OnInit(){
        base.OnInit();
    }

    public void SetDestination(Vector3 position)
    {  
        agent.enabled = true;
        destination = position;
        destination.y = 0.5f;
        agent.SetDestination(position);
        ChangeAnim(Anim.runAnim);
        // Debug.Log("Destination: "+position);
    }

    public override void Attack(Vector3 point){
        Debug.Log("Bot Attack");
        base.Attack(point);
        // SetDestination(transform.position);
        agent.enabled = false;
    }

    public override void Rotate(){
        base.Rotate();
        ChangeState(new AttackState());
    }

    public override void OnDeath(){
        base.OnDeath();
    }

    public void ReturnPool(){
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    IState<Bot> currentState;
    private void Update(){
        if(currentState != null){
            currentState.OnExecute(this);
        }
        // OnAttack(State.half);
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