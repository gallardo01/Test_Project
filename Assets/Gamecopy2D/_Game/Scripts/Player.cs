using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : Character
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int Speed = 250;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    //[SerializeField] private spawnBot spawnBot;

    private bool isGrounded = true;
    private bool isAttack = false;
    [SerializeField] private bool isJumping = false;


    private float horizontal;
    public int coin = 0;

    private Vector3 savePoint;

    private void Awake()
    {
        //coin = PlayerPrefs.GetInt("coin", 0);
    }
    // Update is called once per frame
    void Update()
    {
        
        if (IsDeath)
        {
            return;
        }
        isGrounded = checkGrounded();

        // -1 ->0 -> 1
        // doc phim mui ten
        horizontal = Input.GetAxisRaw("Horizontal"); // chieu ngang
                                                     // verticle = Input.GetAxisRaw("Vertical"); // chieu doc

        if (isAttack)
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
            //jumpin
            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Jump();
            }

            // change anim "run"
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                ChangeAnim("run");
            }

            // attack
            if (Input.GetKey(KeyCode.C))
            {
                Attack();
            }

            // throw
            if (Input.GetKey(KeyCode.F))
            {
                Throw();
            }

        }
        // jumpout
        if (!isGrounded && rb.velocity.y < 0)
        {
            ChangeAnim("fall");
            isJumping = false;
        }
        //move
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeAnim("run");
            rb.velocity = new Vector2(horizontal * Speed, rb.velocity.y);

            transform.rotation = Quaternion.Euler(0, horizontal > 0 ? 0 : 180, 0);
            //transform.localScale = new Vector3(horizontal, 1, 1);
        }
        else if (isGrounded)
        {
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// reset trang thai
    /// </summary>
    public override void OnInit()
    {
        base.OnInit();
        isAttack = false;
        transform.position = savePoint;
        ChangeAnim("idle");
        DeActiveAttack();
        SavePoint();
        UIManager.instance.setCoin(coin);
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        OnInit();
    }

    protected override void OnDead()
    {
        base.OnDead();
    }
    private bool checkGrounded()
    {

        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }

    public void Attack()
    {
        isAttack = true;
        ChangeAnim("attack");
        Invoke(nameof(resetIdle), 0.5f);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Throw()
    {
        isAttack = true;
        ChangeAnim("throw");
        Invoke(nameof(resetIdle), 0.5f);

        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation); // ham tao kunai prefab
    }

    public void Jump()
    {
        isJumping = true;
        ChangeAnim("jump");
        rb.AddForce(jumpForce * Vector2.up);
    }
    internal void SavePoint()
    {
        savePoint = transform.position;
    }

    private void resetIdle()
    {
        isAttack = false;
        ChangeAnim("idle");
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }
    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    public void SetMove(float horizontal)
    {
        this.horizontal = horizontal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "coin")
        {
            coin++;
            PlayerPrefs.SetInt("coin", coin);
            UIManager.instance.setCoin(coin);
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Deadzone")
        {
            ChangeAnim("die");
            Invoke(nameof(OnInit), 1f);
        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "mushroom")
        {
            Debug.Log("mush");
            Vector2 normal = collision.contacts[0].normal;
            if (normal == Vector2.up)
            {
                collision.gameObject.SetActive(false);
                StartCoroutine(RespawnEnemy(collision.gameObject.GetComponent<mushroom>()));
            }
            //else
            //{
            //    Invoke(nameof(OnInit), 0.5f);
            //}

        }
    }
    IEnumerator RespawnEnemy(mushroom spawner)
    {
        //Debug.Log("spawn");
        yield return new WaitForSeconds(4f);
        spawner.SpawnEnemy(); 
    }
    public void setJump(bool x)
    {
        isJumping = x;
    }

}
