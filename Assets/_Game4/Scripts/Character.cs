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
    protected string currentAnim = runAnim;
    [SerializeField] private Animator playerAnim;
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

    void OnInit(){
        changeAnim(idleAnim);
    }

    void SetData(){
        
    }

    void OnDespawn(){

    }

    void OnDeath(){

    }

    void OnKill(){

    }

    protected void Attack(Vector3 startPoint, Vector3 endPoint){
        Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", startPoint, Quaternion.identity).GetComponent<Bullet>();
        bullet.SetDestination(endPoint);
        // EasyObjectPool.instance.ReturnObjectToPool(bullet);
    }

    void UpSize(){

    }

}
