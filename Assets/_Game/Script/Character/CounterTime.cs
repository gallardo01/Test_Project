using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class CounterTime 
{
    UnityAction doneAction;
    public float time;

    public void Start(UnityAction doneAction, float time)
    {
        this.doneAction = doneAction;
        this.time = time;
    }
    public void Excute()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                Exit();
            }
        }
    }
    public void Exit()
    {
        doneAction?.Invoke();
    }
    public void Cancel()
    {
        doneAction = null;
        time = -1;
    }
}
