using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Bot : Character
{

    internal IdleState idle = new IdleState();
    internal MoveState move = new MoveState();
    internal AttackState attack = new AttackState();
    internal DeadState dead = new DeadState();

    [SerializeField] private NavMeshAgent agent;
    private Vector3 destionation;

    IState<Bot> currentState;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
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
    //public override void OnAttack()
    //{
    //    base.OnAttack();
    //    count.Start(ThrowWeapon, 0.4f);
    //    //ChangAnim(Constants.ANIM_IDLE);

    //}
    //public void SpawnNewWayPoint()
    //{

    //    wayPoint = EasyObjectPool.instance.GetObjectFromPool("WayPoint", this.transform.position, Quaternion.identity).GetComponent<WayPoint>();
    //    wayPoint.target = this.transform;
    //    wayPoint.color.color = skinColor.material.color;
    //}
    public override void OnInit()
    {
        base.OnInit();

    }
}
