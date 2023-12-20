using ChangeAnim;
using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Character : AbsCharacter
{
    
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform rightHand;
    [SerializeField] private GameObject weaponObject;

    //[SerializeField] protected GameObject body;
    protected string currentAnim;
    public void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if (currentAnim != null) animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
    public override void OnInit()
    {
        Instantiate(weaponObject, rightHand);
    }
    public override void OnDespawn()
    {

    }
    public override void OnAttack()
    {

    }
    public override void OnDeath()
    {

    }
    public bool CanMove()
    {
        bool canMove = true;
        return canMove;
    }
}

