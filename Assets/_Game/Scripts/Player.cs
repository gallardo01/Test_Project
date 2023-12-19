using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{

    [SerializeField] protected Animator animator;
    [SerializeField] protected float attackRange;
    [SerializeField] protected Transform player;
    [SerializeField] protected float speed;
    [SerializeField] protected Weapon weapon;

    protected string currentAnim;
    protected List<Transform> targets;
    protected Vector3 currentTarget;
    protected bool canAttack;

    protected void ToCallInUpdate() {
        if (targets.Count > 0 && currentAnim == Constants.IDLE_ANIM && canAttack) {
            Attack();
        }
    }

    private void Awake() {
        OnInit();
    }

    protected void OnInit() {
        targets = new List<Transform>();
        currentAnim = Constants.IDLE_ANIM;
        canAttack = true;
    }

    // Remove
    public virtual void OnDespawn() {

    }

    // Run Animation
    public void OnDeath() {
        ChangeAnim(Constants.DEATH_ANIM);
    }

    protected void SetData() {

    }

    protected void OnKill() {

    }

    // Check if current target active
    protected void Attack() {
        currentTarget = targets[0].position;
        transform.LookAt(currentTarget);
        ChangeAnim(Constants.ATTACK_ANIM); 
    }

    public void ChangeAnim(string newAnim) {
        if (newAnim == currentAnim) return;
        animator.ResetTrigger(currentAnim);
        currentAnim = newAnim;
        animator.SetTrigger(newAnim);
    }

    protected void UpSize() {

    }

    public void AddTarget(Collider other) {
        targets.Add(other.transform);
    }

    public void RemoveTarget(Collider other) {
        targets.Remove(other.transform);
    }

    protected void ChangeWeapon() {

    }

    public void Throw() {
        Bullet bullet = BulletPool.Get();
        bullet.transform.position = transform.position;
        bullet.OnInit(this, transform.position + (currentTarget - transform.position).normalized * attackRange);
        canAttack = false;
        Invoke(nameof(AttackReady), 2);
    }

    private void AttackReady() {
        canAttack = true;
    }
}
