using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Pool;

public enum SkinPosition
{
    Hat = 0,
    Accessory = 1,
    Back = 2,
    Pant = 0,
    Set = 1,
}

public class Player : MonoBehaviour
{
    public Transform rightHand;
    public Transform hat;
    public Renderer pant;
    public Transform accessory;
    public Transform leftHand;
    public Transform back;
    public Renderer body;

    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform player;
    [SerializeField] protected Collider collider;
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected WeaponList weaponList;
    [SerializeField] protected Vector3 scoreOffset;
    [SerializeField] protected Transform sprayTransform;
    [SerializeField] protected Transform[] availableSkinPositions;
    [SerializeField] protected Renderer[] skinRenderers;

    protected CameraFollow cameraFollow;
    protected string currentAnim;
    protected Collider[] targets;
    protected Transform currentTarget;
    protected bool canAttack;
    protected int score;
    protected int targetCount;
    protected GameObject scoreObject;
    protected TextMeshProUGUI scoreText;
    protected CounterTime counter;
    protected bool lockTarget;
    protected float attackRange;
    protected Weapon weapon;
    protected Dictionary<string, SkinItem> equippedSkins;
    protected string[] positionNames;

    public Transform[] AvailableSkinPositions => availableSkinPositions;
    public Renderer[] SkinRenderers => skinRenderers;
    public Dictionary<string, SkinItem> EquippedSkin => equippedSkins;

    private void Start()
    {
        counter = new CounterTime();

        // Instantiate the available slots for skin
        equippedSkins = new Dictionary<string, SkinItem>();
    }

    public void SetScoreText(GameObject score)
    {
        scoreObject = score;
        scoreText = scoreObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        scoreText.SetText("0");
    }

    // Call player function to equip/unequip item, dont call skin function
    public void Equip(SkinItem skin)
    {
        string skinType = skin.SkinPosition.ToString();

        skin.Equip(this);
        // Update equippedSkins
        equippedSkins[skinType] = skin;
    }

    // Player responsible for keeping and updating the state
    // Item responsible for showing/hiding the skin
    // When trying skin, call function from the skin to avoid changing player original state
    public void UnEquip(SkinItem skin) {
        string skinType = skin.SkinPosition.ToString();
        if (equippedSkins.ContainsKey(skinType))
        {
            equippedSkins[skinType].UnEquip();
            equippedSkins.Remove(skinType);
        }
    }

    // Check for nearby enemies
    protected void ToCallInUpdate()
    {
        // When attacking, dont check for enemy so that target before attacking and after firing bullet remains the same
        // Not running this -> wont check for nearby enemies -> constant target -> target decoration remains unchanged visually
        if (lockTarget) return;

        targetCount = Physics.OverlapSphereNonAlloc(transform.position, attackRange, targets, playerMask);
        float minDistance = -1;
        for (int i = 0; i < targetCount; i++)
        {
            if (targets[i] == collider) continue;
            float distance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (distance < minDistance || minDistance == -1)
            {
                minDistance = distance;
                currentTarget = targets[i].transform;
            }
        }

        if (minDistance != -1 && currentAnim == Constants.IDLE_ANIM && canAttack) Attack();
    }

    private void LateUpdate()
    {
        // Score deleted when main character die but character is not deleted
        scoreObject.transform.position = Camera.main.WorldToScreenPoint(transform.position + scoreOffset);
    }

    private void OnEnable()
    {
        OnInit();
    }

    protected virtual void OnInit()
    {
        cameraFollow = FindObjectOfType<CameraFollow>();
        score = 0;
        targets = new Collider[LevelManager.Instance.botCount + 1];
        currentAnim = Constants.IDLE_ANIM;
        canAttack = true;
        collider.enabled = true;
        lockTarget = false;
        attackRange = Constants.attackRange;
    }

    // Remove
    public virtual void OnDespawn()
    {
        // Need Pooling
        // Destroy(scoreObject);
        scoreObject.SetActive(false);
    }

    // Run Animation
    public virtual void OnDeath()
    {
        ChangeAnim(Constants.DEATH_ANIM);
        collider.enabled = false;
        LevelManager.Instance.remainingPlayerCount--;
        ParticleSystem spray = Pools.sprayPool.Get();
        spray.transform.SetParent(sprayTransform);
        spray.transform.localPosition = Vector3.zero;
        spray.transform.localRotation = Quaternion.identity;
        enabled = false;
    }

    // Save information of bought items into playerperf
    protected void SetData()
    {
        // PlayerPrefs.SetInt("")
    }

    public virtual void OnKill()
    {
        UpSize();

        // Increase y axis so player not overlap with score
        scoreOffset += Vector3.up * scoreOffset.y * 0.1f;

        score++;
        if (scoreText) scoreText.SetText(score.ToString());
    }

    // Check if current target active
    protected void Attack()
    {
        transform.LookAt(currentTarget.position - Vector3.up * currentTarget.position.y);
        ChangeAnim(Constants.ATTACK_ANIM);
    }

    public void ChangeAnim(string newAnim)
    {
        if (newAnim == currentAnim || !collider.enabled) return;

        // If attack, lock the target
        lockTarget = newAnim == Constants.ATTACK_ANIM;

        animator.ResetTrigger(currentAnim);
        currentAnim = newAnim;
        animator.SetTrigger(newAnim);
    }

    protected void UpSize()
    {
        transform.localScale *= 1.025f;
        attackRange *= 1.025f;
    }

    public void ChangeWeapon(int index)
    {
        if (weapon) Destroy(weapon.gameObject);
        weapon = Instantiate(weaponList.GetWeapon(index), rightHand);
        weapon.OnInit(this);
    }

    public void Throw()
    {
        canAttack = false;
        Invoke(nameof(AttackReady), 2);

        // Max range
        Vector3 target = (currentTarget.position - transform.position).normalized * attackRange + transform.position;
        weapon.Throw(target);
    }

    private void AttackReady()
    {
        canAttack = true;
    }

    internal void CanAttack()
    {
        canAttack = true;
        CancelInvoke();
    }
}
