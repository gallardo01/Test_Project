using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PlayerController : Character
{
    // Start is called before the first frame update
    void Start()
    {
        changeAnim("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && !LevelManager.Ins.isWin)
        {
            Vector3 nextPoints = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if (CanMove(nextPoints))
            {
                transform.position = checkGround(nextPoints);
            }
            // Change anim
            changeAnim("run");
            if (JoystickControl.direct != Vector3.zero)
            {
                body.forward = JoystickControl.direct;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
        }
        if(LevelManager.Ins.isWin){
            transform.position = Vector3.MoveTowards(transform.position, endPoint.position, 0.1f);
        }
    }
}
