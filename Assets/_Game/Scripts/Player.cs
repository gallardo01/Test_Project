using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : Character
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask waterLayer;
    [SerializeField] private float SpeedRun;
    [SerializeField] private float speedAttack = 0.5f;
    [SerializeField] private float jumpForce = 350;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private GameObject timeText;
    [SerializeField] private Tilemap tile_water;
    //[SerializeField] private spawnBot spawnBot;

    [SerializeField] private bool isGrounded = true;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private bool isPotion = false;

    private float horizontal;
    public int coin = 0;
    private float speedDefault = 5f;
    private float speedAttackDefault = 0.5f;
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
        if (isPotion)
        {
            if (checkGrounded() || checkWater())
            {
                isGrounded = true;
            }
            else
                isGrounded = false;

        }
        else
        {
            isGrounded = checkGrounded();
        }
        

        // -1 ->0 -> 1
        // doc phim mui ten
        horizontal = Input.GetAxisRaw("Horizontal"); // chieu ngang
                                                     // verticle = Input.GetAxisRaw("Vertical"); // chieu doc

        if (isAttack)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // attack
        if (Input.GetKey(KeyCode.C))
        {
            Attack();
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
        if ( Mathf.Abs(horizontal) > 0.1f)
        {

            ChangeAnim("run"); 
            rb.velocity = new Vector2(horizontal * SpeedRun, rb.velocity.y);

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
        SpeedRun = speedDefault;
        speedAttack = speedAttackDefault;
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
    private bool checkWater()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, waterLayer);
        return hit.collider != null;
    }

    public void Attack()
    {
        isAttack = true;
        ChangeAnim("attack");
        Invoke(nameof(resetIdle), speedAttack);
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), speedAttack);
        
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
        //isAttack = false;
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
        if(collision.tag == "heathPotion")
        {           
            healHP(30);
            backSpeed();
            Destroy(collision.gameObject);
        }
        if(collision.tag == "Potion")
        {
            UIManager.instance.OnInitTime_text();
            isPotion = true;
            StartCoroutine(endBuff());
            Destroy(collision.gameObject);
            UIManager.instance.SetWaterColliderHard();

        }
        

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "mushroom")
        {
            
            Vector2 normal = collision.contacts[0].normal;
            if (normal == Vector2.up)
            {
                collision.gameObject.SetActive(false);
                StartCoroutine(RespawnMushroom(collision.gameObject.GetComponent<mushroom>()));
            }
            //else
            //{
            //    Invoke(nameof(OnInit), 0.5f);
            //}

        }
    }
    IEnumerator RespawnMushroom(mushroom spawner)
    {
        
        yield return new WaitForSeconds(4f);
        spawner.Spawn(); 
    }
    public void setGround(bool x)
    {
        isGrounded = x;
    }
    public void setJump(bool x)
    {
        isJumping = x;
    }
    public void upSpeed()
    {
        speedAttack *= 0.9f;
        SpeedRun *= 1.1f;
    }
    public void backSpeed()
    {
        speedAttack *= 1.1f;
        SpeedRun *= 0.9f;
    }
    IEnumerator endBuff()
    {
        yield return new WaitForSeconds(10f);
        isPotion = false;
    }
    

}
