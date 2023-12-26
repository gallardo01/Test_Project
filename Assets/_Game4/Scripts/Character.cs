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
    [SerializeField] private Transform body, hand;
    public bool canMove = true;
    [SerializeField] private Weapon yourWeapon;
    // public bool isAttack = false;
    // private int sizeNum = 1;
    public Vector3 enemyPoint;
    void Start(){

    }

    public override void OnInit(){

    }
    public override void OnDespawn(){

    }
    public override void OnDeath(){

    }
    public override void OnAttack(){

    }
    // [SerializeField] protected int speed;
    // [SerializeField] protected LayerMask groundLayer;
    // protected const string idleAnim = "idle";
    // protected const string runAnim = "run";
    // protected const string attackAnim = "attack";
    // public string currentAnim = runAnim;
    // [SerializeField] private Animator playerAnim;
    // [SerializeField] private Transform hand;
    // public bool isAttack = false;
    // private int sizeNum = 1;
    // public Vector3 enemyPoint;
    // // Start is called before the first frame update
    // void Start()
    // {
    //     OnInit();
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    public bool CanMove(Vector3 point)
    {
        return canMove;
    }

    // // change anim -------------------------------------------------------------------
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

    // void OnInit(){
    //     changeAnim(idleAnim);
    // }

    // void SetData(){
        
    // }

    // void OnDespawn(){
        
    // }

    // public void OnDeath(){
    //     changeAnim("die");
    // }

    // void OnKill(){

    // }

    // void OnStopMove(){

    // }

    public void OnAttack(Vector3 endPoint){
        // transform.forward = new Vector3(0, 0, 0);
        Vector3 lookPos = endPoint - transform.position;
        lookPos.y = 0;
        transform.rotation = Quaternion.LookRotation(lookPos);
        changeAnim("attack");
        // Debug.Log("attack");
        // isAttack = true;
        enemyPoint = endPoint;
        // StartCoroutine(IEShooting(endPoint));
    }

    // IEnumerator IEShooting(Vector3 endPoint){
    //     yield return new WaitForSeconds(0.5f);
    //     Shooting();
    // }

    public void OnShoot(){
        Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", hand.position + new Vector3(0,0,0.5f), transform.rotation).GetComponent<Bullet>();
        bullet.SetDestination(enemyPoint);
        canMove = true;
        yourWeapon.Throw();
        // yourWeapon.OnEnabled();

    }

    // void UpSize(){
    //     transform.localScale = Vector3.one * sizeNum;
    // }

}
