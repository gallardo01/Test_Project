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

    [SerializeField] protected float attackRange;
 

    private string currentAnim;
    public Vector3 positionTarget;
    public Character target;
    public float lenghtRaycast;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnInit()
    {

    }

    public void OnDeath()
    {

    }

    public void SetData()
    {

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

    public void ThrowWeapon(Vector3 target)
    {
        ThrowWeapon bullet = EasyObjectPool.instance.GetObjectFromPool("Arrow", transform.position, transform.rotation).GetComponent<ThrowWeapon>();
        bullet.gameObject.SetActive(true);
        bullet.character = this;
        bullet.OnInit();
    }


}
