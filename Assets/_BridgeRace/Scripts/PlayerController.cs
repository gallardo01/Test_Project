using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : Player
{

    // Start is called before the first frame update
    void Start()
    {
        Init();
        currentAnimation = "Idle";
        objectPool = ObjectPool.Instance.Pool;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ChangeAnim("Run");
        }
        if (Input.GetMouseButtonUp(0)) {
            ChangeAnim("Idle");
        }

        stair = null;

        direction = Vector3.zero;

        if (JoystickControl.direct != Vector3.zero) {
            
            direction = JoystickControl.direct;
            
            // Look toward move direction
            body.forward = direction;
            
            // Check stair
            Physics.Raycast(transform.position + direction * speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, stairLayer);
            
            // Update stair color
            if (hit.collider != null)
            {
                stair = hit.collider.gameObject.GetComponent<Stair>();
                if (parent.childCount > 0 && !stair.Filled()) {
                    stair.Fill(colorType);

                    // Remove brick from stack
                    parent.GetChild(parent.childCount - 1).gameObject.GetComponent<Brick>().Deactivate();
                    parent.GetChild(parent.childCount - 1).transform.parent = null;
                }
            }

            // Check for moving
            Physics.Raycast(transform.position + direction * speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, walkable);
        }

        if (hit.collider != null && (!stair || stair.Filled() || direction.z < 0)) {
            transform.position = hit.point;
        }
    }
}
