using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using MarchingBytes;
using Unity.VisualScripting;

public class Character : AbstractCharacter
{
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected int speed;
    [SerializeField] protected LayerMask groundLayer;
    protected string currentAnim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] public Range range;
    public bool isDead = false;
    private int score = 0;
    [SerializeField] GameObject indicatorPrefabs;
    protected TargetIndicator targetIndicator;
    int frameCount = 0;
    //[SerializeField] protected GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAnim(Anim.idleAnim);
    }

    // Update is called once per frame
    void Update()
    {
        frameCount += 1;
    }

    public override void OnInit()
    {
        targetIndicator = Instantiate(indicatorPrefabs, transform.position, Quaternion.identity).GetComponent<TargetIndicator>();

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
            // ChangeAnim(Anim.deadAnim);
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            // Destroy(gameObject);
        }
    }

    public void Rotate() //-------------------------------------------------------------------
    {
        Vector3 directionToTarget = range.target - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0f, directionToTarget.z));
        transform.rotation = lookRotation;
    }

    public virtual void Attack(Vector3 point) //-------------------------------------------------------------------
    {
        Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", rightHand.position, transform.rotation).GetComponent<Bullet>();
        bullet.OnInit(this);
        bullet.SetDestination(range.target);
    }

    public virtual void OnAttack(int percent)
    {
        if(percent == State.half && frameCount == 100)
        {
            frameCount = 0;
            percent = Random.Range(0, 2);

            if(percent == 0)
            {
                percent = State.half;
            }
            if (percent == 1)
            {
                percent = State.all;
            }
        }

        if(percent == State.all)
        {
            if(range.onTarget)
            {
                Rotate();
                ChangeAnim(Anim.attackAnim);
            }
            if(!range.onTarget)
            {
                ChangeAnim(Anim.idleAnim);
            }
        }
        // body.GetComponent<Rigidbody>().useGravity = false;
    }
}
