using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MarchingBytes;

public class Player : Character
{
    [SerializeField] private Transform mainCharacter;
    [SerializeField] private GameObject body;
    public bool isDeath = false;
    
    void Start()
    {
        OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();

        // Cai thien: Moi lan goi OnDeath() moi goi cai phia duoi
        // if (LevelManager.Instance.TotalCharacter == 1 && !isDead)
        // {
        //     GameManager.Instance.EndGame(Status.win);
        // }
    }

    void Moving()
    {
        if (Input.GetMouseButton(0) && !isDeath)
        {
            JoyStickControl();
            // body.GetComponent<Rigidbody>().useGravity = true;
        }
        if (Input.GetMouseButtonUp(0) && !isDeath)
        {
            OnAttack(State.all);
        }
    }

    public override void OnInit(){
        base.OnInit();
        EquipWeapon(2);
    }

    public override void OnDeath(){
        isDeath = true;
        base.OnDeath();
        gameObject.GetComponent<Collider>().enabled = false; // chua hoat dong a?
        GameManager.Instance.EndGame(Status.lose);
        Invoke(nameof(DestroyGameObject), 2f);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
    
    void JoyStickControl()
    {
        Vector3 nextPoint = mainCharacter.position + JoystickControl.direct * speed * Time.deltaTime;

        if (CanMove(nextPoint) && JoystickControl.direct != Vector3.zero)
        {
            // Debug.Log("Acess CanMove");
            ChangeAnim(Anim.runAnim);
            mainCharacter.position = nextPoint;
        }

        if (JoystickControl.direct != Vector3.zero)
        {
            // Debug.Log("Acess Diff");
            mainCharacter.forward = JoystickControl.direct;
        }  
    }
}
