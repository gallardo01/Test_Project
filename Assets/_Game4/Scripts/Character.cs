using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Character : AbstractCharacter
{
    [SerializeField] protected LayerMask groundLayer;
    private string currentAnim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform body, hand, bulletSpot;
    public bool canMove = true;
    [SerializeField] private Weapon yourWeapon;
    public bool isAttack = false;
    [SerializeField] GameObject indicatorPrefabs;
    [SerializeField] GameObject indicatorPoint;
    protected TargetIndicator targetIndicator;
    public Vector3 enemyPoint;
    void Start(){
        OnInit();
    }

    public override void OnInit(){
        Debug.Log("Init Character");
        targetIndicator = Instantiate(indicatorPrefabs, transform.position, Quaternion.identity).GetComponent<TargetIndicator>();
        targetIndicator.OnInit(indicatorPoint.transform);
    }
    public override void OnDespawn(){

    }
    public override void OnDeath(){

    }

    // public override void OnAttack(){

    // }

    public bool CanMove(Vector3 point)
    {
        return canMove;
    }

    public void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            playerAnim.ResetTrigger(currentAnim);
            currentAnim = animName;
            playerAnim.SetTrigger(currentAnim);
            if(currentAnim == "attack"){
                canMove = false;
            }
        }
    }

    public virtual void OnAttack(Vector3 endPoint){
        Vector3 lookPos = endPoint - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
        isAttack = true;
        changeAnim("attack");
        enemyPoint = endPoint;
    }

    public void OnShoot(){
        Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", bulletSpot.position, transform.rotation).GetComponent<Bullet>();
        bullet.OnMove(enemyPoint);
        
        yourWeapon.Throw();
        isAttack = false;
        if(gameObject.GetComponent<Bot>() != null){
            gameObject.GetComponent<Bot>().ChangeState(new IdleState());
        }
    }
}
