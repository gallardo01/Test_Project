using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    private bool isGrounded = true;
    private bool isJumping=false;
    private bool isAttack=false;
    private bool isDeath=false;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce=350f;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private Transform flyPos;

    [SerializeField] private GameObject attackArea;
    
    [SerializeField] private GameObject ActiveCarpet;
    private float horizontal;
    private int coin=0;
    [SerializeField] private LayerMask groudLayer;
    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private LayerMask Carpet;
    private Vector3 savePoint;
    bool isCarpet = false;
    float boost,boostAttack;
    
    
    // Update is called once per frame
    private void Awake()
    {
        savePoint = transform.position;
        coin = PlayerPrefs.GetInt("coin", 0);
        
    }
    void Update()
    {
        boost = (maxHP - hp) / maxHP + 1;
        boostAttack = hp / maxHP;
        if (hp <= 0)
        {
            rb.velocity = Vector3.zero;
        }
        if (isDead || isDeath) return;
        isGrounded = CheckGrounded();
        if(isGrounded == false)
        {
            isGrounded = CheckWater();
        }
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.P))
        {

            ActiveCarpet.SetActive(true);

            isCarpet = true;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ActiveCarpet.SetActive(false);
            isCarpet = false;

        }

        if (isCarpet)
        {
            rb.position = Vector3.Lerp(rb.position, flyPos.position, boost * speed * Time.fixedDeltaTime);
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
            }
            ChangeAnim("idle");
            return;
        }
        if (isAttack && isGrounded)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        if (isGrounded)
        {
            if (isJumping)
            {
                return;
            }
            if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded))
            {
                Jump();
            }
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }
            if (Input.GetKeyDown(KeyCode.C) && isGrounded)
            {
                Attack();
            }
            
            if (Input.GetKeyDown(KeyCode.V) && isGrounded)
            {
                Throw();
            }
        }
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed * boost, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }

    }


    public override void OnInit()
    {
        base.OnInit();
        isDeath = false;
        isAttack = false;
        rb.transform.position = savePoint;
        ChangeAnim("idle");
        DeActiveAttack();
        SavePoint();
        UIManager.instance.SetCoin(0);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }
    protected override void OnDeath()
    {
        base.OnDeath();
    }
    public float GetHP()
    {
        return hp;
    }
    public float GetMaxHP()
    {
        return maxHP;
    }
    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0,-1), 1.1f, groudLayer);
        return hit.collider != null;
    }
    private bool CheckWater()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1), 1.4f, waterLayer);
        return hit.collider != null;
    }
    public void Attack()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f * boostAttack);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }
    public void Throw()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);
        Instantiate(kunaiPrefab,throwPoint.position, throwPoint.rotation);
    }
    public void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
        
    }
    private void ResetAttack()
    {
        ChangeAnim("idle");
        isAttack = false;
    }
    
    internal void SavePoint()
    {
        savePoint = transform.position;
    }
    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            coin++;
            UIManager.instance.SetCoin(coin);
            coin = PlayerPrefs.GetInt("coin", coin);
            Destroy(collision.gameObject);
        }
        if(collision.tag == "DeathZone")
        {
            ChangeAnim("die");
            isDeath = true;
            Invoke(nameof(OnInit), 0.5f);
        }
    }
    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
    void Delay()
    {

    }
}
