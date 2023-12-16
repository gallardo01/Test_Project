using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject body;
    [SerializeField] private Transform hand;

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (Input.GetMouseButton(0))
        {
            JoyStickControl();
            changeAnim(runAnim);
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnim(idleAnim);
        }
    }

    void JoyStickControl()
    {
        Vector3 nextPoint = player.position + JoystickControl.direct * speed * Time.deltaTime;

        if (CanMove(nextPoint))
        {
            Debug.Log("Acess CanMove");
            player.position = nextPoint;
        }

        if (JoystickControl.direct != Vector3.zero)
        {
            Debug.Log("Acess Diff");
            player.forward = JoystickControl.direct;
        }  
    }

    public void OnAttack(Vector3 des){
        Attack(hand.position, des);
    }

}
