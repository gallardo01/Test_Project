using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject attackArea;
    [SerializeField] private GameObject healthPotion;
    [SerializeField] private GameObject waterPotion;
    [SerializeField] private GameObject enemySight;
    [SerializeField] private Transform fallCheck;
    [SerializeField] private GameObject key;
    
    private IState currentState;
    private bool dropKey;
    private Collider2D collider2D;
    private bool colliding;
    private bool isRight = true;
    private Character target;

    public Character Target => target;
    private IObjectPool<Enemy> objectPool;
    private int occupiedIndex;

    public IObjectPool<Enemy> ObjectPool { set => objectPool = value; }
    public int OccupiedIndex { set => occupiedIndex = value; get => occupiedIndex; }

    private void Awake() {
        dropKey = false;
        collider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Update() {
        if (currentState != null) {
            currentState.OnExecute(this);
        }

        if (colliding && CheckFall()) ChangeDirection(!isRight); 
        
        if (Input.GetKeyDown(KeyCode.Return)) {
            dropKey = true;
            Debug.Log(2);
        }
    }

    public void Deactivate() {
        objectPool.Release(this);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        colliding = true;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "EnemyWall") {
            ChangeDirection(!isRight);
            DeActiveSight();
            target = null;
            Invoke(nameof(ActiveSight), 5f);
        }
    }

    private bool CheckFall() {
        RaycastHit2D hit = Physics2D.Raycast(fallCheck.position, Vector3.down, 1);
        return hit.collider == null;
    }

    private void DeActiveAttack() {
        attackArea.SetActive(false);
    }

    private void ActiveAttack() {
        attackArea.SetActive(true);
    }

    private void DeActiveSight() {
        enemySight.SetActive(false);
    }

    private void ActiveSight() {
        enemySight.SetActive(true);
    }

    protected override void OnDeath()
    {
        ChangeState(null);
        rb.velocity = Vector2.zero;
        base.OnDeath();
    }

    internal void SetTarget(Character character)
    {
        this.target = character;

        if (IsTargetInRange()) {
            ChangeState(new AttackState());
        } else if (target != null) {
            ChangeState(new PatrolState());
        } else {
            ChangeState(new IdleState());
        }
    }

    public override void OnInit()
    {
        base.OnInit();

        ChangeState(new IdleState());

        DeActiveAttack();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();

        // Drop potion
        int r1 = UnityEngine.Random.Range(0, 2), r2 = UnityEngine.Random.Range(0, 5);
        if (dropKey) Invoke(nameof(DropKey), 0.5f);
        else if (r1 == 1) Invoke(nameof(CreateHealthPotion), 0.5f);
        else {
            if (r2 == 3) Invoke(nameof(CreateWaterPotion), 0.5f);
            if (r2 == 2 && PlayerPrefs.GetInt("stage") == 2) Invoke(nameof(DropKey), 0.5f);
        }
        healthBar.gameObject.SetActive(false);

        Deactivate();
    }

    private void DropKey() {
        Instantiate(key, transform.position, Quaternion.identity);
    }

    private void CreateWaterPotion() {
        Instantiate(waterPotion, transform.position, Quaternion.identity);
    }

    private void CreateHealthPotion() {
        Instantiate(healthPotion, transform.position, Quaternion.identity);
    }
    
    public void ChangeState(IState newState) {
        if (currentState != null) {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null) {
            currentState.OnEnter(this);
        }
    }

    private void OnEnable() {
        Physics2D.IgnoreCollision(collider2D, Game2DController.Instance.PlayerCollider);
        hp = 100;
        healthBar.OnInit(100);
        healthBar.gameObject.SetActive(true);
        colliding = false;
        OnInit();
    }

    public void Moving() {
        ChangeAnim("run");
        rb.velocity = transform.right * moveSpeed;
    }

    public void StopMoving() {
        ChangeAnim("idle");
        rb.velocity = Vector2.zero;
    }

    public void Attack() {
        ChangeAnim("attack");

        ActiveAttack();

        Invoke(nameof(DeActiveAttack), 0.5f);
    }

    public bool IsTargetInRange() {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange) {
            return true;
        } else {
            return false;
        }
    }

    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }

    
}
