using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using MarchingBytes;
public class IdleState : IState<Bot>
{
    public void OnEnter(Bot t)
    {
        t.SetDestionation(t.transform.position);
      
    }

    public void OnExecute(Bot t)
    {
       
    }

    public void OnExit(Bot t)
    {

    }

}
