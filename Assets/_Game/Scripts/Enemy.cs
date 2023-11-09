using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea, Blood;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded = true;

    // Start is called before the first frame update
    private IState currentState;
    private bool isRight = true;
    private Character target;
    public Character Target => target;
    // void Start()
    // {
    //     OnInit();
    // }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null && !IsDead){
            // Debug.Log("Hello");
            currentState.OnExecute(this);
        }

        isGrounded = CheckGrounded();

        //ChangeAnim("fall");
        if(isGrounded == false)
        {
            rb.gravityScale = 100;
        }
        else
        {
            rb.gravityScale = 1;
        }
        
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, groundLayer);
        return hit.collider != null;
    }

    public override void OnInit(){
        base.OnInit();

        ChangeState(new IdleState());
        DeActiveAttack();
    }

    public override void  OnDespawn(){
        base.OnDespawn();
        Destroy(healthBar.gameObject);
        Destroy(gameObject);
        Instantiate(Blood, transform.position, Quaternion.identity);
    }

    protected override void OnDeath(){
        ChangeState(null);
        base.OnDeath();
    }

    public void ChangeState(IState newState){
        if(currentState != null){
            currentState.OnExit(this);
        }
        currentState = newState;
        if(currentState != null){
            currentState.OnEnter(this);
        }
    }

    public void Moving(){
        // Debug.Log("run");

        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving(){
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack(){
        ChangeAnim("attack");
        ActiveAttack();
        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public bool IsTargetInRange(){
        // if(target != null){
        //     Debug.Log(Vector2.Distance(target.transform.position, transform.position));
        // }
        if(target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange){
            return true;
        }
        else{
            return false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "EnemyWall")
        {
            ChangeDirection(!isRight);
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    private void ActiveAttack()
    {
        attackArea.SetActive(true);
    }

    private void DeActiveAttack()
    {
        attackArea.SetActive(false);
    }

    internal void SetTarget(Character character)
    {
        // throw new NotImplementedException();
        this.target = character;
        if(IsTargetInRange())
        {
            ChangeState(new AttackState());
        }
        else if(Target != null)
        {
            ChangeState(new PatrolState());
        }
        else
        {
            ChangeState(new PatrolState());
        }
    }

}
