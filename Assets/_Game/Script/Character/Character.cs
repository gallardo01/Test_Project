using MarchingBytes;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] internal LayerMask characterLayer;
    [SerializeField] protected GameObject playerSkin;
    [SerializeField] protected GameObject WeaponImg;
    [SerializeField] protected SphereCollider sphere;

    
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

    public void OnInit()
    {
        SetData();
    }

    public void OnDeath()
    {

    }

    public void SetData()
    {
        sphere.radius = 3.5f;
    }



    public void Onkill()
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

    public void UpSize()
    {

    }
   
    public void AddTarget(Character target)
    {
        targets.Add(target);
    }
    public void RemoveTarget(Character target)
    {
        targets.Remove(target);
        target = null;
    }

    public Character GetTarget()
    {
        if(targets.Count > 0)
        {
            target = targets[UnityEngine.Random.Range(0, targets.Count)];
            return target;
        }
        return null;
    }
    //public bool CheckEnemy()
    //{
    //    Collider[] Enemys = Physics.OverlapSphere(transform.position, attackRange, characterLayer);
    //    Array.Sort(Enemys, new ColliderDistanceComparer(transform.position));
    //    if(Enemys.Length > 1 ) {
    //        target = Cache.GetScript(Enemys[1]);
    //    }
    //    return Enemys.Length > 1;
    //}
    public void ThrowWeapon()
    {
        ThrowWeapon bullet = EasyObjectPool.instance.GetObjectFromPool("Candy", transform.position + transform.forward*0.8f, transform.rotation).GetComponent<ThrowWeapon>();
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 50f);
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
