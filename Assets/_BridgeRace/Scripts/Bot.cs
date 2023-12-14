using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : Player
{
    private Vector3 destination;

    // public Camera cam;
    public NavMeshAgent agent;

    public bool IsDestination => Vector3.Distance(new Vector3(destination.x, transform.position.y, destination.z), transform.position) < 0.1f;

    private void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
            direction = agent.desiredVelocity.normalized;
            CanMove(transform.position);
        }

        if (direction != Vector3.zero) body.forward = direction;

    }

    private void CanMove(Vector3 position)
    {
        if (Physics.Raycast(transform.position + direction * agent.speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, stairLayer))
        {
            stair = Cache.GetStair(hit.collider);
            if (!stair.Filled() || stair.color != colorType) {
                if (parent.childCount > 0)
                {
                    stair.Fill(colorType);
                    canMove = true;

                    // Remove brick from stack
                    Cache.GetBrick(parent.GetChild(parent.childCount - 1)).Deactivate();
                    parent.GetChild(parent.childCount - 1).parent = null;
                } else if (currentState.GetType() == typeof(AttackState)) agent.ResetPath();
            }
        }
    }

    public void ChangeState(IState<Bot> state)
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

    public void SetDestination(Vector3 position)
    {
        destination = position;
        agent.SetDestination(destination);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == Constant.PLAYER_TAG && 
        canCollide && Cache.GetPlayer(other).CanCollide && 
        parent.childCount < Cache.GetPlayer(other).Parent.childCount &&
        !agent.isOnOffMeshLink &&
        parent.childCount > 0) {
            agent.ResetPath();
            ChangeAnim("Falling");
            enabled = false;
            agent.enabled = false;
            canCollide = false;

            DropBrick();

            Invoke(nameof(ReEnable), 3f);
            Invoke(nameof(EnableCollide), 5f);
        }
    }

    private void ReEnable() {
        enabled = true;
        agent.enabled = true;
        ChangeAnim("Idle");
        agent.SetDestination(destination);
    }

    private void EnableCollide() {
        canCollide = true;
    }

    private void Awake()
    {
        Init();
    }

    IState<Bot> currentState;
    IEnumerator Start()
    {
        agent.autoTraverseOffMeshLink = false;
        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                yield return StartCoroutine(NormalSpeed(agent));
                agent.CompleteOffMeshLink();
            }
            yield return null;
        }
    }

    IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 endPos = data.endPos + Vector3.up * agent.baseOffset;

        while (agent.transform.position != endPos)
        {
            var dir = endPos - transform.position;
            direction = new Vector3(dir.x, 0, dir.z).normalized;
            Check();
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }
    }
}
