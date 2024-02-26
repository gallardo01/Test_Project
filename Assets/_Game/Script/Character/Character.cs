using MarchingBytes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    private string currentAnim;
    private CounterTime counterTime = new CounterTime();
    public CounterTime count => counterTime;
    
    public Transform indicatorPoint;
    public TargetIndicator targetIndicator;
    public int score;
    public int deadScore;
    public float currentScale;
    
    public string nameCharacter;
    public Weapon WeaponImg;
    public Transform WeaponPoint;
    public bool IsWeapon;
    public PoolType typeWeapon;

    public Transform HatPoint;
    public Transform ShieldPoint;
    public Renderer PanType;

    public bool isUlti;
    public override void OnInit()
    {

        IsWeapon = true;
        
        
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
        ThrowWeapon bullet = SimplePool.Spawn<ThrowWeapon>(typeWeapon, transform.position + Vector3.up*1f + transform.forward*1f,transform.rotation);
        if (bullet != null)
        {
            bullet.character = this;
            bullet.target = this.target.transform;
            bullet.OnInit();
            this.PostEvent(EventID.ThrowWeapon);
        }
    }
    public void RotateTarget()
    {
        
        positionTarget = target.TF.position;
        Vector3 targetAngle = positionTarget - TF.position;
        float targetAngleY = Mathf.Atan2(targetAngle.x, targetAngle.z) * Mathf.Rad2Deg;
        TF.rotation = Quaternion.Euler(0f, targetAngleY, 0f);
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngleY, 0f);
        TF.rotation = Quaternion.Slerp(TF.rotation, targetRotation, Time.deltaTime * 50f);
    }
    public virtual bool checkTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange, characterLayer);
        colliders = colliders.OrderBy(c => Vector3.Distance(c.bounds.center, transform.position)).ToArray();
        if(colliders.Length > 1)
        {
            target = Cache.GetScript(colliders[1]);
        }
        return colliders.Length > 1;

    }
    public  virtual void  ChangeWeaponImg()
    {
        
        foreach (Transform child in WeaponPoint)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i <LevelManager.Instance.weapons.Count; i++)
        {
            
            if (LevelManager.Instance.weapons[i].weaponType == typeWeapon)
            {
                WeaponImg = Instantiate(LevelManager.Instance.weapons[i]);
               
                WeaponImg.transform.SetParent(WeaponPoint);
                WeaponImg.transform.localPosition = Vector3.zero;
                WeaponImg.transform.localRotation = Quaternion.Euler(Vector3.zero);

            }
        }
    }
    public virtual void GrowthCharacter()
    {
        for (int i = 0; i < LevelManager.Instance.basePoints.Count - 1; i++)
        {
            if (score >= LevelManager.Instance.basePoints[i].Score && score < LevelManager.Instance.basePoints[i + 1].Score)
            {
                currentScale = LevelManager.Instance.basePoints[i].Scale;
                deadScore = LevelManager.Instance.basePoints[i].DeadScore;
            }
            if (score >= LevelManager.Instance.basePoints.Last().Score)
            {
                currentScale = LevelManager.Instance.basePoints[^1].Scale;
                deadScore = LevelManager.Instance.basePoints.Last().DeadScore;
            }
        }
        this.TF.localScale = Vector3.one * currentScale;
    }

    public void UpdateScore(int score)
    {
        this.score += score;
        targetIndicator.setScore(this.score);
    }

    public virtual void BuffUlti()
    {
        attackRange += 3f;
    }

    public virtual void EndBuff()
    {
        isUlti = false;
    }
}




