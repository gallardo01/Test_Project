using MarchingBytes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : Character
{ 
    [SerializeField] Rigidbody rb;
    [SerializeField] public float speed;
    public GameObject circleAttack;
    
    //private float defaultAttackRange =5f;
    //private float defaultSpeed = 5f;

    public GameObject hatCurrent;
    public Material pantCurrent;

    public override void OnInit()
    {
        base.OnInit();
        this.transform.position = new Vector3(0, 0, 0);
        this.transform.rotation = Quaternion.Euler(0, 180f, 0);
        this.collider.enabled = true;
        ResetData();
        this.target = null;
        score = 1;
        nameCharacter = "you";
        targetIndicator.textName.text = this.nameCharacter;
        GrowthCharacter();
//        this.ChangeSaveItem();
        targetIndicator.OnInit();
        targetIndicator.setScore(score);
        
    }
    public void ResetData()
    {
        this.speed = 5f;
        this.attackRange = 7f;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay))
        {
            if (Input.GetMouseButtonDown(0))
            {
                count.Cancel();
                IsWeapon = true;
            }
            if (Input.GetMouseButton(0) && JoystickControl.direct != Vector3.zero)
            {
                rb.MovePosition(rb.position + JoystickControl.direct * speed * Time.deltaTime);
                transform.position = rb.position;
                transform.forward = JoystickControl.direct;
                ChangAnim(Constants.ANIM_RUN);
            }
            else
            {
                count.Excute();
            }
            if (Input.GetMouseButtonUp(0))
            {
                ChangAnim(Constants.ANIM_IDLE);
            }
            if (!Input.GetMouseButton(0))
            {
                if (checkTarget() && IsWeapon)
                {
                    //Debug.Log("attack");
                    ChangAnim(Constants.ANIM_IDLE);
                    RotateTarget();
                    if (isUlti)
                    {
                        ChangAnim(Constants.ANIM_ULTI);
                    }
                    else
                    {
                        ChangAnim(Constants.ANIM_ATTACK);
                    }
                    OnAttack();
                }
            }
        }
    }


    public override void OnAttack()
    {
        
        base.OnAttack();
        IsWeapon = false;
        count.Start(ThrowWeapon, 0.35f);
        //ChangAnim(Constants.ANIM_IDLE);
  
    }
    public override void GrowthCharacter()
    {
        base.GrowthCharacter();
        attackRange = 5f * currentScale;
        speed = 5f * currentScale;
        
    }


    public override void BuffUlti()
    {
        base.BuffUlti();
        circleAttack.transform.localScale = new Vector3(attackRange / 1.2f, attackRange / 1.2f, attackRange / 1.2f);
    }

    public override void EndBuff()
    {
        base.EndBuff();
        circleAttack.transform.localScale = new Vector3(3f, 3f, 1f);
    }

    public void ChangeSaveItem()
    {
        
        if (SaveManager.Instance.currentHat != -1)
        {
            hatCurrent = Instantiate(DataManager.Instance.hatDatas[SaveManager.Instance.currentHat].Prefabs, HatPoint);
            attackRange += DataManager.Instance.hatDatas[SaveManager.Instance.currentHat].AttackRange*0.1f;
        }
        if (SaveManager.Instance.currentPant != -1)
        {
            pantCurrent = DataManager.Instance.panDatas[SaveManager.Instance.currentPant].Material;
            PanType.material = pantCurrent;
            speed += DataManager.Instance.panDatas[SaveManager.Instance.currentPant].Speed*0.2f;
        }
        int index = SaveManager.Instance.currentWeapon;
        this.typeWeapon = LevelManager.Instance.GetCurrentWeapon(index).weaponType;
        this.attackRange += LevelManager.Instance.weapons[SaveManager.Instance.currentWeapon].weaponData.AttackRange * 0.1f;
        this.speed += LevelManager.Instance.weapons[SaveManager.Instance.currentWeapon].weaponData.Speed * 0.2f;
        base.ChangeWeaponImg();
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        this.collider.enabled = false;
        this.ChangAnim(Constants.ANIM_DIE);
    }
}
