using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using MarchingBytes;

public class Character : AbstractCharacter
{
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected int speed;
    [SerializeField] protected LayerMask groundLayer;
    protected string currentAnim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] protected Range range;
    public bool isDead = false;
    //[SerializeField] protected GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnim(AnimConstant.idleAnim);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInit()
    {
        
    }

    public override void OnDespawn()
    {
        
    }

    public bool CanMove(Vector3 point)
    {
        if (Physics.Raycast(point + Vector3.up * 0.2f, Vector3.down, 5f, groundLayer))
        {
            return true;
        }
        return false;
    }

    public void ChangeAnim(string animName) //-------------------------------------------------------------------
    {
        if (currentAnim != animName)
        {
            playerAnim.ResetTrigger(currentAnim);
            currentAnim = animName;
            playerAnim.SetTrigger(currentAnim);
        }
    }


    // void OnTriggerEnter(Collider other) //-------------------------------------------------------------------
    // {
    //     if (other.tag == Tag.bulletTag)
    //     {
    //         Debug.Log("Bullet Hit");
    //         isDead = true;
    //         OnDeath();
    //         // other.gameObject.GetComponent<Bullet>().DestroyBullet();
    //     }
    // }

    public override void OnDeath() //-------------------------------------------------------------------
    {
        if (!isDead) // isDead khong can thiet
        {
            Debug.Log("Die Bitch!!");
            // ChangeAnim(AnimConstant.deadAnim);
            Destroy(gameObject);
        }
    }

    public void Rotate() //-------------------------------------------------------------------
    {
        Vector3 directionToTarget = range.target - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = lookRotation;
    }

    public virtual void Attack(Vector3 point) //-------------------------------------------------------------------
    {
        Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", rightHand.position, transform.rotation).GetComponent<Bullet>();
        bullet.OnInit(this);
        bullet.SetDestination(range.target);
        // if (range.onTarget)
        // {
        //     bullet.SetDestination(range.target);
        // }
        // if (!range.onTarget)
        // {
        //     bullet.SetDestination(bullet.transform.position + transform.forward * 10f);
        // }
        //bullet.OnShoot();
    }

    public void OnAttack()
    {
        if(range.onTarget)
        {
            Rotate();
            ChangeAnim(AnimConstant.attackAnim);
        }
        if(!range.onTarget)
        {
            ChangeAnim(AnimConstant.idleAnim);
        }
        // body.GetComponent<Rigidbody>().useGravity = false;
    }
}
