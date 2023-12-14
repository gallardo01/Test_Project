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

    private void SeekTarget(Bot b) {
        Brick brick = SeekBrickPoint(b);

        if (brick != null) {
            b.SetDestination(brick.transform.position);
            b.ChangeAnim("Run");
        } else b.ChangeAnim("Idle");
    }

    private Brick SeekBrickPoint(Bot b) {
        if (b.Stage == null) return null;
        foreach (Brick br in b.Stage.Bricks) {
            if (br.ColorType == b.ColorType) return br;
        }
        return null;
    }

}
