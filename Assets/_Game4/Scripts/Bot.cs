using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Character
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IState<Bot> currentState;
    public void ChangeState(IState<Bot> state){
        if (currentState != null){
            ChangeState(new AttackState());
        }
    }

}
