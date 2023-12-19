using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using MarchingBytes;

public class Character : MonoBehaviour
{
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected int speed;
    [SerializeField] protected LayerMask groundLayer;
    protected string currentAnim = "run";
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

    
    void OnOnit()
    {

    }

    protected bool CanMove(Vector3 point)
    {
        if (Physics.Raycast(point + Vector3.up * 0.2f, Vector3.down, 5f, groundLayer))
        {
            return true;
        }
        return false;
    }

    protected void ChangeAnim(string animName) //-------------------------------------------------------------------
    {
        if (currentAnim != animName)
        {
            playerAnim.ResetTrigger(currentAnim);
            currentAnim = animName;
            playerAnim.SetTrigger(currentAnim);
        }
    }

    void OnDespawn()
    {
        
    }

    void OnTriggerEnter(Collider other) //-------------------------------------------------------------------
    {
        if (other.tag == Tag.bulletTag)
        {
            isDead = true;
        }
    }

    protected void OnDeath() //-------------------------------------------------------------------
    {
        if (isDead)
        {
            ChangeAnim(AnimConstant.deadAnim);
            Destroy(gameObject);
        }
    }

    public void Attack() //-------------------------------------------------------------------
    {
        ChangeAnim(AnimConstant.attackAnim);
        Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", rightHand.position, transform.rotation).GetComponent<Bullet>();
        if (range.onTarget)
        {
            bullet.SetDestination(range.target);
        }
        if (!range.onTarget)
        {
            bullet.SetDestination(bullet.transform.position + transform.forward * 10f);
        }
        //bullet.OnShoot();
    }

    void OnAttack()
    {
        // if(detectTarget())
        // {
        //     Attack();
        // }
    }
}
