using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState<Bot>
{

    int targetBrick;

    public void OnEnter(Bot t)
    {
        t.ChangeAnim("Run");
        targetBrick = 5;
        t.SetDestination(t.transform.position);
    }

    public void OnExecute(Bot t)
    {
        if (t.IsDestination) {
            if (t.Parent.childCount >= targetBrick) {
                t.ChangeState(new AttackState());
            } else {
                SeekTarget(t);
            }
        }
    }

    public void OnExit(Bot t)
    {

    }

    private void SeekTarget(Bot t) {
        Brick brick = SeekBrickPoint(t);

        if (brick != null) {
            t.SetDestination(brick.transform.position);
        }
    }

    private Brick SeekBrickPoint(Bot b) {
        foreach (Brick br in BrickSpawner.Ins.Bricks[b.Level]) {
            if (br.ColorType == b.ColorType) return br;
        }
        return null;
    }

}
