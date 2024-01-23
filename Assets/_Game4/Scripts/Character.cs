using MarchingBytes;
using UnityEngine;

public class Character : AbstractCharacter
{
    [SerializeField] public GameObject midPoint;
    [SerializeField] protected Transform rightHand;
    [SerializeField] protected int speed;
    [SerializeField] protected LayerMask groundLayer;
    protected string currentAnim;
    [SerializeField] private Animator playerAnim;
    [SerializeField] public Range range;
    public bool isDead = false;
    private int score = 0;

    public Transform indicatorPoint;
    public TargetIndicator targetIndicator;
    int frameCount = 0;
    public bool isAttack = false;
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
        // targetIndicator = Instantiate(indicatorPrefabs, transform.position, Quaternion.identity).GetComponent<TargetIndicator>();
        // targetIndicator.OnInit(indicatorPoint);
        targetIndicator = LevelManager.Instance.CreateIndicatorPanel(indicatorPoint, range);
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
        // if (!isDead) // isDead khong can thiet
        // {
        //     Debug.Log("Die Bitch!!");
        //     // ChangeAnim(Anim.deadAnim);
        //     EasyObjectPool.instance.ReturnObjectToPool(gameObject);
        //     // Destroy(gameObject);
        // }
        isDead = true;
        targetIndicator.gameObject.SetActive(false);
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
        ChangeAnim(Anim.deadAnim);
        LevelManager.Instance.totalBot--;
        LevelManager.Instance.UpdateUi();
        Coin coin = EasyObjectPool.instance.GetObjectFromPool("Coin", transform.position, transform.rotation).GetComponent<Coin>();
    }

    public virtual void Rotate() //-------------------------------------------------------------------
    {
        if (range.onTarget)
        {
            Vector3 directionToTarget = (Vector3)range.target - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0f, directionToTarget.z));
            transform.rotation = lookRotation;
        }
    }

    public virtual void Attack(Vector3 point) //-------------------------------------------------------------------
    {
        if (range.target != null)
        {
            Bullet bullet = EasyObjectPool.instance.GetObjectFromPool("Bullet", rightHand.position, transform.rotation).GetComponent<Bullet>();
            bullet.OnInit(this);
            bullet.SetDestination((Vector3)range.target);
        }
    }

    public virtual void OnAttack(int percent)
    {
        if (percent == State.all)
        {
            if (range.onTarget)
            {
                Rotate();
                ChangeAnim(Anim.attackAnim);
            }
            if (!range.onTarget)
            {
                ChangeAnim(Anim.idleAnim);
            }
        }
        // body.GetComponent<Rigidbody>().useGravity = false;
    }
}
