using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using MarchingBytes;

public class Character : MonoBehaviour
{
    [SerializeField] protected int speed;
    [SerializeField] protected LayerMask groundLayer;
    protected string currentAnim = "run";
    [SerializeField] private Animator playerAnim;
    //[SerializeField] protected GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        changeAnim(AnimConstant.idleAnim);
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

    // change anim -------------------------------------------------------------------
    protected void changeAnim(string animName)
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
    //
    void OnDeath()
    {

    }
    // detect target -------------------------------------------------------------------
    // bool detectTarget(int level)
    // {
    //     return true;
    // }

    // Attack -------------------------------------------------------------------
    public void Attack()
    {
        changeAnim(AnimConstant.attackAnim);
        Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", transform.position, Quaternion.identity).GetComponent<Bullet>();
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
