using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{

    // bat dau state
    void OnEnter(Enemy enemy);
    // update state
    void OnExcute(Enemy enemy);
    // thoat state
    void OnExit(Enemy enemy);
}
