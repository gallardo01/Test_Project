using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Rigidbody2D rb;
    // [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Kunai kunaiPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private float speed = 5f;
    private bool isGrounded = true;
    private bool isJumping = false;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private Animator attackAnim;
    private float immortalTime = 1.3f;
    // private bool isDeath = false;

    private float horizontal;
    // private string currentAnimName;
    private int coin = 0;
    private Vector3 savePoint;
    [SerializeField] private float jumpForce = 350;

    // Start is called before the first frame update
    void Awake()
    {
        coin = PlayerPrefs.GetInt("coin", 0);
        // hp = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        // if(isDeath){
        //     return;
        // }
        if(IsDead){
            return;
        }
    
        isGrounded = CheckGrounded();

        horizontal = Input.GetAxisRaw("Horizontal");

        if(isAttack){
            rb.velocity = Vector2.zero;
            // isAttack = true;
            return;
        }
        if(Input.GetKeyDown(KeyCode.X)){
            // isAttack = true;
            Attack();
        }
        if(Input.GetKeyDown(KeyCode.V)){
            Throw();
        }
        if(isGrounded){
            // isAttack = false;
            if(isJumping){
                return;
            }
            // Jump
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
                Jump();
            }
            // Change Anim run
            if(Mathf.Abs(horizontal) > 0.1f){
                ChangeAnim("run");
            }

            // Attack
            // if(Input.GetKeyDown(KeyCode.X)){
            //     // isAttack = true;
            //     Attack();
            // }

            // Throw
            if(hp < 30){
                speed = 800;
                attackAnim.speed = 2;
            }
            else{
                speed = 500;
                attackAnim.speed = 1;
            }

        }
        // Change anim fall
        if(!isGrounded && rb.velocity.y < 0 && !isAttack){
            ChangeAnim("fall");
            isJumping = false;
        }
        // Debug.Log(CheckGrounded());
        // Moving
        if(Mathf.Abs(horizontal) > 0.1f){
            // ChangeAnim("run");
            rb.velocity = new Vector2(horizontal * Time.deltaTime * speed, rb.velocity.y);
        
            // transform.localScale = new Vector3(horizontal, 1, 1);
            transform.rotation = Quaternion.Euler(new Vector3(0,horizontal > 0 ? 0 : 180, 0));
        }
        // Idle
        else if(isGrounded && !isAttack){
            ChangeAnim("idle");
            rb.velocity = Vector2.zero;
        }
    }

    public override void OnInit(){
        base.OnInit();
        isAttack = false;
        // isDeath = false;

        transform.position = savePoint;
        ChangeAnim("idle");
        DeActiveAttack();

        SavePoint();
        UiManager.instance.SetCoin(coin);
    }

    public override void  OnDespawn(){
        base.OnDespawn();
        OnInit();
    }

    protected override void OnDeath(){
        base.OnDeath();
    }

    // public override void OnHit(float damage){
    //     if(!isImmortal){
    //         base.OnHit();
    //     }
    // }

    public void SetMove(float horizontal){
        this.horizontal = horizontal;
    }

    private bool CheckGrounded(){
        // Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.1f, Color.red);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);

        // if(hit.collider != null){    
        //     return true;
        // }
        // return false;
        return hit.collider != null;
    }
    
    public void Attack(){
        ChangeAnim("attack");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);

        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public void Throw(){
        ChangeAnim("throw");
        isAttack = true;
        Invoke(nameof(ResetAttack), 0.5f);

        Instantiate(kunaiPrefab, throwPoint.position, throwPoint.rotation);
    }

    public void EnterImmortalMode(){
        if(!isImmortal){
            isImmortal = true;
            Invoke(nameof(ExitImmortalMode), immortalTime);
            UiManager.instance.SetShieldTimer(immortalTime);
        }
    }

    public void ExitImmortalMode(){
        isImmortal = false;
    }

    private void ResetAttack(){
        ChangeAnim("idle");
        isAttack = false;
    }

    public void Jump(){
        if(isGrounded){
            isJumping = true;
            ChangeAnim("jump");
            rb.AddForce(jumpForce * Vector2.up);
        }
    }

    // private void ChangeAnim(string animName){
    //     if(currentAnimName != animName){
    //         anim.ResetTrigger(animName);
    //         currentAnimName = animName;
    //         anim.SetTrigger(currentAnimName);
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Coin"){
            coin++;
            PlayerPrefs.GetInt("coin", coin);

            UiManager.instance.SetCoin(coin);
            Destroy(other.gameObject);
        }
        if(other.tag == "Healing"){
            hp += 30;
            UpdateHealth();
            Destroy(other.gameObject);
        }
        if(other.tag == "WaterRunning"){
            Destroy(other.gameObject);
            UiManager.instance.SetWaterTimer(10);
        }
        if(other.tag == "DeathZone"){
            // isDeath = true;
            ChangeAnim("die");

            Invoke(nameof(OnInit), 1f);
        }
    }

    private void ActiveAttack(){
        attackArea.SetActive(true);
    }

    private void DeActiveAttack(){
        attackArea.SetActive(false);
    }

    internal void SavePoint(){
        savePoint = transform.position;
    }

    

}
