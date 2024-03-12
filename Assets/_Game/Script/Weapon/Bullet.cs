using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : GameUnit
{
    public Transform target;
    public Character character;
    public Character Victim;
    public Vector3 startPoint;
    protected float speed;
    [SerializeField] protected Transform child;


    public override void OnInit()
    {
        if(character.isUlti == true)
        {
            speed = Constants.SpeedBulletUlti;
        }
        else
        {
            speed = Constants.SpeedBulletDefault;
        }
        this.TF.localScale = Vector3.one * character.currentScale;
        speed = this.character.attackRange * 1.2f;
        TF.forward = (target.position - TF.position + Vector3.up*1f).normalized;
        startPoint = this.character.TF.position;
    }


    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        character.WeaponImg.OnEnable();
        character.IsWeapon = true;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_OBSTACLE) && !this.character.isUlti)
        {
            this.OnDespawn();
        }
        if (other.CompareTag(Constants.TAG_BOT) && this.character.gameObject != other.gameObject)
        {
            if (this.character.isUlti)
            {
                character.EndBuff();
            }
            this.Victim = Cache.GetScript(other);
            this.PostEvent(EventID.OnEnemyDead, this);

        }
        if (other.CompareTag(Constants.TAG_PLAYER) && this.character.gameObject != other.gameObject)
        {
            if (character.isUlti)
            {
                character.EndBuff();
            }
            Player player = other.GetComponent<Player>();
            OnDespawn();
            player.OnDespawn();
            this.PostEvent(EventID.Lose, this.character);
        }
    }
    protected void Rotate()
    {
        int speed = 500;
        TF.Rotate(0f, speed * Time.deltaTime, 0f, Space.World);
    }

}
