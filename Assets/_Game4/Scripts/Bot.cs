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
        ChangeState(new PatrolState());
    }

    // Update is called once per frame
    public void SetDestination(Vector3 position)
    {
        agent.enabled = true;
        destination = position;
        destination.y = 1f;
        agent.SetDestination(position);
        changeAnim("run");
        // Debug.Log("Destination: "+position);
    }

    public override void OnAttack(Vector3 point){
        base.OnAttack(point);
        isAttack = true;
        SetDestination(transform.position);
        agent.enabled = false;
        ChangeState(new AttackState());
    }

    public override void OnDeath(){
        base.OnDeath();
        Invoke(nameof(DestroyBot), 1f);
    }

    void DestroyBot(){
        EasyObjectPool.instance.ReturnObjectToPool(gameObject);
    }

    IState<Bot> currentState;
    private void Update(){
        if(currentState != null && !isDead){
            currentState.OnExecute(this);
        }
    }

    public void ChangeState(IState<Bot> state){
        if(currentState != null){
            currentState.OnExit(this);
        }
        currentState = state;
        if(currentState != null && !isDead){
            currentState.OnEnter(this);
        }
    }

    // private OnAttack(int randomPercent){
    //     int randomNum = 0;
    //     if(randomPercent == 50){
    //         randomNum = Random.Range(0,2);
    //     }
    //     else if(randomPercent == 100){
    //         randomNum = 1;
    //     }
    //     if(onTarget && randomPercent == 1){
    //         // attack
    //     }
    //     if(!onTarget){
    //         // no attack
    //     }
    // }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Bullet"){
            OnDeath();
        }
    }

}