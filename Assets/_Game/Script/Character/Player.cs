using MarchingBytes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Player : Character
{ 
    [SerializeField] Rigidbody rb;
    [SerializeField] public float speed;
    [SerializeField] private GameObject circleAttack;
    private bool isRun;
    //private float defaultAttackRange =5f;
    //private float defaultSpeed = 5f;

    public GameObject hatCurrent;
    public Material pantCurrent;
    // Start is called before the first frame update
    void Start() 
    {
        OnInit();        
    }
    public override void OnInit()
    {
        base.OnInit();
        this.collider.enabled = true;
        ResetData();
        score = 1;
        deadScore = 1;
        currentScale = 1;
        nameCharacter = "you";
        targetIndicator.textName.text = this.nameCharacter;
        GrowthCharacter();
        this.ChangeSaveItem();
        targetIndicator.OnInit();
         targetIndicator.setScore(score);
        
    }
    public void ResetData()
    {
        this.speed = 5f;
        this.attackRange = 5f;
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
                    ChangAnim(Constants.ANIM_ATTACK);
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
    public override void UpScore(int addScore)
    {
        base.UpScore(addScore);
        
    }

    public void ChangeSaveItem()
    {
        //Debug.Log("getSave");
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
}
