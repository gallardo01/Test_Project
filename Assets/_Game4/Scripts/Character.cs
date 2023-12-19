using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected int speed;
    [SerializeField] protected LayerMask groundLayer;
    protected const string idleAnim = "idle";
    protected const string runAnim = "run";
    protected const string attackAnim = "attack";
    public string currentAnim = runAnim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] private Transform hand;
    public bool isAttack = false;
    private int sizeNum = 1;
    public Vector3 enemyPoint;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected bool CanMove(Vector3 point)
    {
        if (Physics.Raycast(point + Vector3.up * 0.2f, Vector3.down, 5f, groundLayer) && !isAttack)
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

    void OnInit(){
        changeAnim(idleAnim);
    }

    void SetData(){
        
    }

    void OnDespawn(){
        
    }

    public void OnDeath(){
        changeAnim("die");
    }

    void OnKill(){

    }

    void OnStopMove(){

    }

    public void OnAttack(Vector3 endPoint){
        transform.forward = endPoint - transform.position;
        changeAnim(attackAnim);
        // Debug.Log("attack");
        isAttack = true;
        enemyPoint = endPoint;
        // StartCoroutine(IEShooting(endPoint));
    }

    IEnumerator IEShooting(Vector3 endPoint){
        yield return new WaitForSeconds(0.5f);
        Shooting();
    }

    public Bullet Shooting(){
        isAttack = false;
        Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", hand.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.SetDestination(enemyPoint);
        return bullet;
    }

    void UpSize(){
        transform.localScale = Vector3.one * sizeNum;
    }

}
