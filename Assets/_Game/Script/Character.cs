using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class Character : AbsCharacter
{

    public Animator animator;
    private string currentAnim;
    public Transform body;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool CanMove(Vector3 point)
    {
        bool canMove = true;
        return canMove;
    }

    public void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

    public override void OnInit()
    {
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
}
