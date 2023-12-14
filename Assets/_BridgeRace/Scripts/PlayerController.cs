using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : Player
{

    private void Start() {
        Init();
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ChangeAnim("Run");
        }
        if (Input.GetMouseButtonUp(0)) {
            ChangeAnim("Idle");
        }

        if (JoystickControl.direct != Vector3.zero) {
            
            direction = JoystickControl.direct;
            
            // Look toward move direction
            body.forward = direction;
        }

        Check();

        if (hit.collider != null && canMove) transform.position = hit.point;

        Reset();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.tag == Constant.PLAYER_TAG && 
        canCollide && Cache.GetPlayer(other).CanCollide && 
        parent.childCount < Cache.GetPlayer(other).Parent.childCount
        && parent.childCount > 0) {
            ChangeAnim("Falling");
            enabled = false;
            canCollide = false;

            DropBrick();

            Invoke(nameof(ReEnable), 3f);
            Invoke(nameof(EnableCollide), 5f);
        }
    }

    private void ReEnable() {
        enabled = true;
        ChangeAnim("Idle");
    }

    private void EnableCollide() {
        canCollide = true;
    }
}
