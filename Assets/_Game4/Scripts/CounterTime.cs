using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.Events;
using UnityEngine;

public class CounterTime
{
    UnityAction doneAction;
    private float time;

    public bool IsRunning => time > 0;
    // Start is called before the first frame update
    public void Start(UnityAction doneAction, float time)
    {
        this.doneAction = doneAction;
        this.time = time;
    }

    // Update is called once per frame
    public void Execute()
    {
        if(time > 0){
            time -= Time.deltaTime;
            if(time <= 0){
                Exit();
            }
        }
    }

    public void Exit(){
        doneAction?.Invoke();
    }

    public void Cancel(){
        doneAction = null;
        time = -1;
    }
}
