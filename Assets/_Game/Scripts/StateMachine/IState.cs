using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState 
{
    void OnEnter(Enermy enemy);
    void OnExecute(Enermy enemy);
    void OnExit(Enermy enemy);
}
