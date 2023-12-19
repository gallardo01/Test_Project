using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Player
{
    private void Update() {
        ToCallInUpdate();
    }

    public override void OnDespawn()
    {
        BotPool.Release(this);
    }

}
