using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    private Transform target;
    private Vector3 direct;
    private Character character;
    public Bot Victim;
    private Vector3 startPoint;
    public float speed;
    [SerializeField] private Transform child;
    private float range;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void OnInit(Character character, Transform target)
    {
        this.target = target;
        this.character = character;
        this.range = character.attackRange;
        startPoint = this.character.transform.position;
        transform.forward = (target.position - transform.position).normalized;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6, Space.Self);

        if (Vector3.Distance(this.transform.position, startPoint) > this.range)
        {
            Remove();
        }
    }

    public void Remove()
    {
        EasyObjectPool.instance.ReturnObjectToPool(transform.gameObject);
        character.WeaponImg.OnEnable();
        character.IsWeapon = true;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.tag);
        //Debug.Log(other.transform);
        if (other.CompareTag(Constants.TAG_BOT))
        {
            //Debug.Log(2);
            Bot bot = other.GetComponent<Bot>();
            this.Victim = bot;
            this.PostEvent(EventID.OnEnemyDead, this);

        }
        //if (other.CompareTag(Constants.TAG_PLAYER))
        //{
        //    Debug.Log("player");
        //    Player player = other.GetComponent<Player>();
        //    Remove();
        //    //player.collider.enabled = false;
        //    //player.gameObject.SetActive(false);
        //}
    }

}
