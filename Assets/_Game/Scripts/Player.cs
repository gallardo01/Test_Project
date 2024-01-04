using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Pool;

public class Player : MonoBehaviour
{
    public Transform hand;
    public float attackRange;

    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform player;
    [SerializeField] protected Weapon weapon;
    [SerializeField] protected Collider collider;
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected WeaponList weaponList;
    [SerializeField] protected Vector3 scoreOffset;
    [SerializeField] protected ParticleSystem sprayOnDeath;

    protected CameraFollow cameraFollow;
    protected string currentAnim;
    protected Collider[] targets;
    protected Transform currentTarget;
    protected bool canAttack;
    protected int score;
    protected int targetCount;
    protected GameObject scoreObject;
    protected TextMeshProUGUI scoreText;

    public void SetScoreText(GameObject score) {
        scoreObject = score;
        scoreText = scoreObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        scoreText.SetText("0");
    }

    protected void ToCallInUpdate()
    {
        targetCount = Physics.OverlapSphereNonAlloc(transform.position, attackRange, targets, playerMask);
        float minDistance = -1;
        for (int i = 0; i < targetCount; i++)
        {
            if (targets[i] == collider) continue;
            float distance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (distance < minDistance || minDistance == -1)
            {
                minDistance = distance;
                currentTarget = targets[i].transform;
            }
        }

        if (minDistance != -1 && currentAnim == Constants.IDLE_ANIM && canAttack) Attack();
    }

    private void LateUpdate() {
        scoreObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + scoreOffset);
    }

    private void OnEnable()
    {
        OnInit();
    }

    protected void OnInit()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
        score = 0;
        weapon.OnInit(this);
        targets = new Collider[LevelManager.Instance.botCount + 1];
        currentAnim = Constants.IDLE_ANIM;
        canAttack = true;
        collider.enabled = true;
    }

    // Remove
    public virtual void OnDespawn()
    {
        Destroy(scoreObject);
    }

    // Run Animation
    public virtual void OnDeath()
    {
        ChangeAnim(Constants.DEATH_ANIM);
        collider.enabled = false;
        LevelManager.Instance.remainingBotCount--;
    }

    protected void SetData()
    {

    }

    public virtual void OnKill()
    {
        UpSize();
        score++;
        if (scoreText) scoreText.SetText(score.ToString());
    }

    // Check if current target active
    protected void Attack()
    {
        transform.LookAt(currentTarget.position - Vector3.up * currentTarget.position.y);
        ChangeAnim(Constants.ATTACK_ANIM);
    }

    public void ChangeAnim(string newAnim)
    {
        if (newAnim == currentAnim || !collider.enabled) return;
        animator.ResetTrigger(currentAnim);
        currentAnim = newAnim;
        animator.SetTrigger(newAnim);
    }

    protected void UpSize()
    {
        transform.localScale *= 1.025f;
        attackRange *= 1.025f;
    }

    protected void ChangeWeapon(int index)
    {
        Destroy(weapon.gameObject);
        weapon =  Instantiate(weaponList.GetWeapon(index), hand);
        weapon.OnInit(this);
    }

    public void Throw()
    {
        canAttack = false;
        Invoke(nameof(AttackReady), 2);

        weapon.Throw(currentTarget.position);
    }

    private void AttackReady()
    {
        canAttack = true;
    }
}
