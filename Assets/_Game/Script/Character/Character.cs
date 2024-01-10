using MarchingBytes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Character : GameUnit 
{

    [SerializeField] private Animator anim;
    [SerializeField] internal LayerMask characterLayer;
    [SerializeField] protected GameObject playerSkin;
    public Renderer skinColor;
    public Collider collider;
    public float attackRange;
    public Vector3 direct;
    public List<Character> targets = new List<Character>();
    public Vector3 positionTarget;
    public Character target;
    public Weapon WeaponImg;
    public bool IsWeapon;
    public PoolType typeWeapon;
    private CounterTime counterTime = new CounterTime();
    public CounterTime count => counterTime;
    
    public Transform indicatorPoint;
    public TargetIndicator targetIndicator;
    public int score;
    private string currentAnim;
    public string nameCharacter;
    public override void OnInit()
    {
        //SetData();
        IsWeapon = true;
        score = 0;
        attackRange = 3f;
        targetIndicator.setScore(score);
        
    }

    //public override void OnDeath()
    //{

    //}

    public void SetData()
    {
        
    }



    public virtual void OnAttack()
    {

    }
    public override void OnDespawn()
    {
        
    }
    public void ChangAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(animName);

        }
    }

    public virtual void UpScore(int addScore)
    {
        score += addScore;
        targetIndicator.setScore(score);
        attackRange++;
        this.transform.localScale = Vector3.one + Vector3.one * (score * 0.2f);

    }
    //public void UpSize(int levelAdd)
    //{
    //    level += levelAdd;
    //    float radius = sphere.radius += levelAdd;
    //    circleAttack.transform.localScale = new Vector3(radius / 3, radius / 3, radius / 3);
    //}


    public void GetTargetIndicator()
    {
        this.nameCharacter = Name.GetName();
        targetIndicator = SimplePool.Spawn<TargetIndicator>(PoolType.Indicator);
        targetIndicator.target = this.indicatorPoint;
        targetIndicator.textName.text = this.nameCharacter;
        targetIndicator.OnInit();
    }
    public void ThrowWeapon()
    {
        WeaponImg.OnDisable();
        ThrowWeapon bullet = SimplePool.Spawn<ThrowWeapon>(typeWeapon, transform.position + Vector3.up*1f,transform.rotation);
        if (bullet != null)
        {
            bullet.character = this;
            bullet.target = this.target.transform;
            bullet.OnInit();
            
        }
    }
    public void RotateTarget()
    {
        positionTarget = target.transform.position;
        Vector3 directionToTarget = positionTarget - transform.position;
        directionToTarget.y = 0f;

        if (directionToTarget != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 100f);
        }
    }
    public bool checkTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, characterLayer);
        colliders = colliders.OrderBy(c => Vector3.Distance(c.bounds.center, transform.position)).ToArray();
        if(colliders.Length > 1)
        {
            target = Cache.GetScript(colliders[1]);
        }
        return colliders.Length > 1;

    }
}




