using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask ground;
    [SerializeField] private GameObject kunaiPrefab;
    [SerializeField] private Transform kunaiSpawnPoint;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    
    private bool isGrounded = true;
    private bool isJumping = false;
    private bool isAttacking = false;
    private bool isDead = false;
    private bool berserk = false;

    private float horizontal;
    private float resetAttackTimer = 1f;

    private Vector3 savePoint;
    // Start is called before the first frame update

    public override void OnInit()
    {
        base.OnInit();
        isAttacking = false;
    
        transform.position = savePoint;
        Changeanim("Idle");

        InActiveAttack();
        SavePoint();
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

    // Update is called once per frame
    void Update()
    {
        isGrounded = Checkground();

        horizontal = Input.GetAxisRaw("Horizontal");

        //if (IsDead)
        //{
        //    return;
        //}

        if (IsDead)
        {
            return;
        }

        if (healthbar.GetHp() <= 50f && !berserk)
        {
            berserk = true;
            speed = speed * 2;
        }

        if (isAttacking)
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

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            if (Mathf.Abs(horizontal) > 0.1f && isGrounded)
            {
                Changeanim("Run");
            }

            if (Input.GetKeyDown(KeyCode.J) && isGrounded)
            {
                Attack();
            }

            if (Input.GetKeyDown(KeyCode.K) && isGrounded)
            {
                Throw();
            }
        }

        if (!isGrounded && rb.velocity.y < 0.1f)
        {
            Changeanim("Fall");
            isJumping = false;
        }

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed * Time.deltaTime, rb.velocity.y);
            transform.rotation = Quaternion.Euler(0, horizontal > 0 ? 0 : 180, 0);
        }
        else if (!isJumping && isGrounded)
        {
            Changeanim("Idle");
            rb.velocity = new Vector2(0, -2);
        }
    }
    private bool Checkground()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, ground);
        return hit.collider != null;
    }

    public void Attack()
    {
        Changeanim("Attack");
        isAttacking = true;
        ActiveAttack();
        Invoke(nameof(ResetAttack), 0.5f);

        Invoke(nameof(InActiveAttack), resetAttackTimer);
    }

    public void Throw()
    {
        Changeanim("Throw");
        isAttacking = true;
        Invoke(nameof(ResetAttack), resetAttackTimer);

        Instantiate(kunaiPrefab, kunaiSpawnPoint.position, kunaiSpawnPoint.rotation);
    }

    private void ResetAttack()
    {
        Changeanim("Idle");
        isAttacking = false;
    }

    public void Jump()
    {
        isJumping = true;
        Changeanim("Jump");
        rb.AddForce(Vector2.up * jumpForce);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Deathzone"))
        {
            isDead = true;
            Changeanim("Die");

            Invoke(nameof(OnInit), 1f);
        }
    }
    
    private void EnterBerserk()
    {
        speed = speed*2;
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void InActiveAttack()
    {
        attackArea.SetActive(false);
    }

    internal void SavePoint()
    {
        savePoint = transform.position;
    }
}
