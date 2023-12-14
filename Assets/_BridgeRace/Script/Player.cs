using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class Player : Character
{
    
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            nextPoint = transform.position + JoystickControl.direct * speed * Time.deltaTime;
            if (CanMove(nextPoint)) transform.position = check(nextPoint);
            if (JoystickControl.direct != Vector3.zero)
            {
                changeAnim("run");
                body.transform.forward = JoystickControl.direct;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            changeAnim("idle");
        }
    }
}
   