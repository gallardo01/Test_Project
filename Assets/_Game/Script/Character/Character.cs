using MarchingBytes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Character : AbsCharacter 
{

    [SerializeField] private Animator anim;
    [SerializeField] internal LayerMask characterLayer;
    [SerializeField] protected GameObject playerSkin;
    [SerializeField] protected SphereCollider sphere;
    public Renderer skinColor;
    public Collider collider;
    public float attackRange => sphere.radius;
    public Vector3 direct;
    public List<Character> targets = new List<Character>();
    public Vector3 positionTarget;
    public Character target;
    public Weapon WeaponImg;
    public bool IsWeapon;

    private CounterTime counterTime = new CounterTime();
    public CounterTime count => counterTime;
    
    public Transform indicatorPoint;
    public TargetIndicator targetIndicator;
    private int score = 0;
    private string currentAnim;
    public override void OnInit()
    {
        //SetData();
        IsWeapon = true;
        targetIndicator = EasyObjectPool.instance.GetObjectFromPool("Indicator", transform.position, Quaternion.identity).GetComponent<TargetIndicator>();
        targetIndicator.gameObject.SetActive(true);
        targetIndicator.OnInit(this.indicatorPoint);
    }

    public override void OnDeath()
    {

    }

    public void SetData()
    {
        sphere.radius = 3.6f;
    }



   public override void OnAttack()
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

    //public void UpSize(int levelAdd)
    //{
    //    level += levelAdd;
    //    float radius = sphere.radius += levelAdd;
    //    circleAttack.transform.localScale = new Vector3(radius / 3, radius / 3, radius / 3);
    //}
   

    //public void AddTarget(Character target)
    //{
    //    targets.Add(target);
    //}
    //public void RemoveTarget(Character target)
    //{
    //    //Debug.Log(gameObject.name);
    //    targets.Remove(target);
    //    target = null;
    //}

    //public Character GetTarget()
    //{
    //    if(targets.Count > 0)
    //    {
    //        target = targets[UnityEngine.Random.Range(0, targets.Count)];
    //        if (target != null)
    //        {
    //            return target;
    //        }
    //        return target;
    //    }
    //    return null;
    //}

    public void ThrowWeapon()
    {
        WeaponImg.OnDisable();
        ThrowWeapon bullet = EasyObjectPool.instance.GetObjectFromPool("Candy", transform.position  + transform.forward*1f, transform.rotation).GetComponent<ThrowWeapon>();
        if (bullet != null)
        {
            bullet.gameObject.SetActive(true);
            bullet.OnInit(this, target.transform);
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 200f);
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




