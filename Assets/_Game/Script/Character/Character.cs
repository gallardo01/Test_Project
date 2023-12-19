using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{

    [SerializeField] private Animator anim;
    [SerializeField] protected LayerMask characterLayer;
    [SerializeField] protected GameObject playerSkin;
    [SerializeField] protected GameObject WeaponImg;
    [SerializeField] protected float speed;


    public float attackRange;
    public float timeCountat;
    public float timeat = 1f;
    public Vector3 positionTarget;
    public Character target;
    public float lenghtRaycast;
    public bool isAttack;
    public bool isRun;
    public Vector3 direct;
    private string currentAnim;
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
        attackRange = 5f;
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
    public void Attack()
    {
        if (Vector3.Distance(direct, Vector3.zero) >= 0.00001f)
        {
            isRun = true;

        }
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, characterLayer);
        if (enemies.Length > 1)
        {
            ChangAnim(Constants.ANIM_ATTACK);
            target = Cache.GetScript(enemies[1]);
            positionTarget = target.transform.position;
            timeCountat += Time.deltaTime;
            if (timeCountat >= timeat && target != null)
            {

                timeCountat = 0;
                ThrowWeapon();

            }

        }

    }
    public bool CheckEnemy()
    {
        Collider[] Enemys = Physics.OverlapSphere(transform.position, attackRange, characterLayer);
        return Enemys.Length > 1;
    }
    public void ThrowWeapon()
    {
        ThrowWeapon bullet = EasyObjectPool.instance.GetObjectFromPool("Arrow", transform.position, transform.rotation).GetComponent<ThrowWeapon>();
        if (bullet != null)
        {
            
            bullet.gameObject.SetActive(true);
            bullet.character = this;
            bullet.OnInit();
        }
    }


}
