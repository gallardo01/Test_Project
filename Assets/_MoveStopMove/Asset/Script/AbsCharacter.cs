using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsCharacter : MonoBehaviour
{
    public abstract void OnInit();
    public abstract void OnDespawn();
    public abstract void OnAttack();
    public abstract void OnDeath();
}
