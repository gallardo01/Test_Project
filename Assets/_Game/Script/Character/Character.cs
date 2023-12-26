using MarchingBytes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : AbsCharacter 
{

    [SerializeField] private Animator anim;
    [SerializeField] internal LayerMask characterLayer;
    [SerializeField] protected GameObject playerSkin;
    public Weapon WeaponImg;
    [SerializeField] protected SphereCollider sphere;

    public Renderer skinColor;
    private int level;
    public Collider collider;
    public float attackRange => sphere.radius;

    public float lenghtRaycast;
    public Vector3 direct;
    private string currentAnim;
    public List<Character> targets = new List<Character>();
    public Vector3 positionTarget;
    public Character target;
    private CounterTime counterTime = new CounterTime();
    public CounterTime count => counterTime;
    // Start is called before the first frame update

    public override void OnInit()
    {
        SetData();
    }

    public override void OnDeath()
    {

    }

    public void SetData()
    {
        sphere.radius = 3.6f;
        level = 1;
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
   

    public void AddTarget(Character target)
    {
        targets.Add(target);
    }
    public void RemoveTarget(Character target)
    {
        Debug.Log(gameObject.name);
        targets.Remove(target);
        target = null;
    }

    public Character GetTarget()
    {
        if(targets.Count > 0)
        {
            target = targets[UnityEngine.Random.Range(0, targets.Count)];
            //if(target != null)
            //{
            //    return target;
            //}
            return target;
        }
        return null;
    }

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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 150f);
        }
    }
}



public class ColliderDistanceComparer : IComparer<Collider>
{
    private Vector3 m_ComparePosition;

    public ColliderDistanceComparer(Vector3 comparePosition)
    {
        m_ComparePosition = comparePosition;
    }

    public int Compare(Collider x, Collider y)
    {
        float xDistance = Vector3.Distance(m_ComparePosition, x.transform.position);
        float yDistance = Vector3.Distance(m_ComparePosition, y.transform.position);
        return xDistance.CompareTo(yDistance);
    }
}
