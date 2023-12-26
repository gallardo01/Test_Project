using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbsCharacter
{

    public Animator animator;
    private string currentAnim;
    public Transform body;
    public LayerMask groundLayer;
    public List<Character> targets = new List<Character>();
    protected Character target;
    [SerializeField] GameObject bulletPrefabs;
    [SerializeField] GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Throw()
    {
        //weapon.SetActive(false);
        Bullet bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.OnInit(this, target.transform);
        weapon.GetComponent<Weapon>().Throw();
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

    public virtual void AddTarget(Character target)
    {
        targets.Add(target);
    }

    public virtual void RemoveTarget(Character target)
    {
        targets.Remove(target);
        this.target = null;
    }

    public Character GetTargetInRange()
    {
        if (targets.Count > 0)
        {
            target = targets[Random.Range(0, targets.Count)];
            return target;
        }
        return null;
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
