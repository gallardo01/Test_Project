using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowWeapon : GameUnit
{
    public Transform target;
    private Vector3 direct;
    public Character character;
    public Bot Victim;
    public Vector3 startPoint;
    public float speed = 8f;
    [SerializeField] private Transform child;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public override void OnInit()
    {
        
        transform.forward = (target.position - transform.position + Vector3.up*1f).normalized;
        startPoint = this.character.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6, Space.Self);
        if (Vector3.Distance(this.transform.position, startPoint) > this.character.attackRange)
        {
            OnDespawn();
        }
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        character.WeaponImg.OnEnable();
        character.IsWeapon = true;

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(Constants.TAG_BOT) && this.character.gameObject != other.gameObject)
        {

            Bot bot = other.GetComponent<Bot>();
            this.Victim = bot;
            this.character.UpScore(Victim.score);
            this.PostEvent(EventID.OnEnemyDead, this);

        }
        if (other.CompareTag(Constants.TAG_PLAYER) && this.character.gameObject != other.gameObject)
        {
            Debug.Log(this.character.name);
            Player player = other.GetComponent<Player>();
            OnDespawn();
            player.collider.enabled = false;
            player.gameObject.SetActive(false);
        }
    }

}
