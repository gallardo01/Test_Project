using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : ThrowWeapon
{
    [SerializeField] float speedRotate;
    public override void OnInit()
    {
        base.OnInit();
        speedRotate = 700f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(0f, speedRotate * Time.deltaTime, 0f, Space.World);
        if (Vector3.Distance(this.transform.position, startPoint) > this.character.attackRange)
        {
            if (character.isUlti)
            {
                character.EndBuff();
            }
            OnDespawn();
        }
    }
}
