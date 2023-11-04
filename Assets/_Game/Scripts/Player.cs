using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class Player : Character
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float speedDecreaseRate;
    [SerializeField] private float glideSpeed;

    private bool isGrounded = true;
    private bool climbing = false;
    private bool isJumping = false;
    private bool isAttack = false;
    private bool isDeath = false;
    private bool jump = false;
    private int coin = 0;
    private Vector3 savePoint;
    private float horizontal;
    private float vertical;

    private void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
        savePoint = transform.position;
    }

    private void FixedUpdate()
    {

        if (jump)
        {
            Jump();
            jump = false;
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        // idle
        else if (isGrounded)
        {
            rb.velocity = Vector2.up * rb.velocity.y;
        }

        if (isAttack) rb.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = CheckGrounded();
        if (climbing) climbing = !isGrounded;
        if (IsDead || isDeath) return;

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (isAttack)
        {
            return;
        }

        if (isGrounded && !climbing)
        {
            PlayAnimator();
            if (isJumping)
            {
                return;
            }

            // change anim run
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            // attack
            if (Input.GetKeyDown(KeyCode.C))
            {
                Attack();
                return;
            }

            // throw
            if (Input.GetKeyDown(KeyCode.V))
            {
                Throw();
                return;
            }

        }

        if (isGrounded || climbing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = true;
                climbing = false;
            }
        }

        // check falling
        if (!isGrounded && rb.velocity.y < 0 && !climbing)
        {
            if (Input.GetKey(KeyCode.Space)) 
            {
                ChangeAnim("glide");
                rb.velocity = new Vector2(rb.velocity.x, glideSpeed);
            }
            else
            {
                ChangeAnim("fall");
            }
            isJumping = false;
        }

        // Moving
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        // idle
        else
        {
            if (isGrounded)
            {
                ChangeAnim("idle");
                rb.velocity = Vector2.zero;
            } else if (!climbing) {
                // rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if (climbing)
        {
            rb.velocity = new Vector2(horizontal * speed / 2, vertical * speed / 2);
            if (Mathf.Abs(horizontal) <= 0.1f && Mathf.Abs(vertical) <= 0.1f) PauseAnimator();
            else PlayAnimator();
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;
        isDeath = false;

        transform.position = savePoint;

        SavePoint();

        ChangeAnim("idle");
        DeActiveAttack();

        UIManager.instance.SetCoin(coin);
    }

    private bool CheckGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        return hit.collider != null;
    }

    public void Attack()
    {
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);

        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Throw()
    {
        ChangeAnim("throw");
        isAttack = true;

        Invoke(nameof(ResetAttack), 0.5f);

        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    public void Jump()
    {
        isJumping = true;
        isGrounded = false;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
    }

    private void ResetAttack()
    {
        isAttack = false;
        ChangeAnim("idle");
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
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

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.SetCoin(coin);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Death Zone")
        {
            ChangeAnim("die");
            isDeath = true;
            Invoke(nameof(OnInit), 1f);
        }
        else if (other.tag == "Rope")
        {
            climbing = true;
            ChangeAnim("climb");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Rope")
        {
            climbing = false;
            if (isGrounded) ChangeAnim("jump");
            PlayAnimator();
        }
    }
}
