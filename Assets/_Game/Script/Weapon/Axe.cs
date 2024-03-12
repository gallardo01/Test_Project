using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : Bullet
{
    public override void OnInit()
    {
        base.OnInit();
    }

    // Update is called once per frame
    void Update()
    {
        
        TF.Translate(TF.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6, Space.Self);
        if (Vector3.Distance(this.TF.position, startPoint) > this.character.attackRange)
        {
            if (character.isUlti)
            {
                character.EndBuff();
            }
            OnDespawn();
        }
    }
}
