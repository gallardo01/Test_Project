using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowWeapon : GameUnit
{
    public Transform target;
    public Character character;
    public Bot Victim;
    public Vector3 startPoint;
    public float speed = 8f;
    [SerializeField] protected Transform child;

    
    public override void OnInit()
    {
        this.TF.localScale = Vector3.one * character.currentScale;
        speed = this.character.attackRange * 1.2f;
        transform.forward = (target.position - transform.position + Vector3.up*1f).normalized;
        startPoint = this.character.transform.position;
    }


    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        character.WeaponImg.OnEnable();
        character.IsWeapon = true;

    }

    private void OnTriggerEnter(Collider other)
    {

        //if (other.CompareTag(Constants.TAG_BOT) && this.character.gameObject != other.gameObject)
        //{

        //    Bot bot = other.GetComponent<Bot>();
        //    this.Victim = bot;
        //    this.PostEvent(EventID.OnEnemyDead, this);

        //}
        //if (other.CompareTag(Constants.TAG_PLAYER) && this.character.gameObject != other.gameObject)
        //{
        //    Player player = other.GetComponent<Player>();
        //    OnDespawn();
        //    player.collider.enabled = false;
        //    player.gameObject.SetActive(false);
        //}
    }
    protected void Rotate()
    {
        int speed = 500;
        TF.Rotate(0f, speed * Time.deltaTime, 0f, Space.World);
    }

}
