using MarchingBytes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : Character
{ 
    [SerializeField] Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject circleAttack;
    private bool isRun;
    private float defaultAttackRange;
    private float defaultSpeed;
    // Start is called before the first frame update
    void Start() 
    {
        OnInit();
        
    }
    public override void OnInit()
    {
        base.OnInit();
        this.collider.enabled = true;
        defaultAttackRange = 5f;
        defaultSpeed = 5f;
        speed = defaultSpeed;
        attackRange = defaultAttackRange;
        score = 1;
        deadScore = 1;
        currentScale = 1;
        nameCharacter = "you";
        targetIndicator.textName.text = this.nameCharacter;
        GrowthCharacter();
        this.ChangeWeaponImg();
        targetIndicator.OnInit();
         targetIndicator.setScore(score);
        
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
        attackRange = defaultAttackRange * currentScale;
        speed = defaultSpeed * currentScale;
    }
    public override void UpScore(int addScore)
    {
        base.UpScore(addScore);
        
    }

    public override void ChangeWeaponImg()
    {
        int index = PlayerPrefs.GetInt("Weapon");
        this.typeWeapon = UIManager.Instance.GetCurrentWeapon(index).GetComponent<Weapon>().weaponType;
        base.ChangeWeaponImg();
    }
}
