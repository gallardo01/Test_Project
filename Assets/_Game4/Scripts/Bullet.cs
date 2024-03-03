using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using MarchingBytes;
using System;
using System.Threading.Tasks;

public class Bullet : MonoBehaviour
{
    public Vector3 destination;
    private bool canMove = false;
    protected Character character;
    [SerializeField] public GameObject[] weaponsList;
    public int weaponIndex;
    // Start is called before the first frame update
    void Start()
    {
        // destination = transform.position + transform.forward * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.gameStatus)
        {
            // gameObject.SetActive(false);
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
        }
        if (GameManager.Instance.gameStatus)
        {
            CanMove();

            if (canMove)
            {
                // Debug.Log("canMove = true");
                OnShoot();
                if (transform.position == destination + transform.forward * 10f)
                {
                    canMove = false;
                    EasyObjectPool.instance.ReturnObjectToPool(gameObject);
                }
            }
        }
    }

    // void Check()
    // {
    //     if (range.onTarget)
    //     {
    //         SetDestination(range.target);
    //     }
    //     if (!range.onTarget)
    //     {
    //         SetDestination(transform.position + transform.forward * 10f);
    //     }
    // }
    public void OnInit(Character characterOnInit, Vector3 point)
    {
        character = characterOnInit;
        GameObject bullet = EasyObjectPool.instance.GetObjectFromPool(weaponsList[weaponIndex].gameObject.name, point, transform.rotation);
    }
    void CanMove()
    {
        canMove = true;
    }

    public void OnShoot()
    {   
        // Debug.Log("OnShoot");
        transform.position = Vector3.MoveTowards(transform.position, destination + transform.forward * 10f, 0.05f);
    }

    public void SetDestination(Vector3 target)
    {
        destination = target;
        destination = new Vector3(target.x, character.midPoint.transform.position.y, target.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tag.characterTag)
        {
            other.gameObject.GetComponent<Character>().OnDeath();
            // Destroy(other.gameObject);
            EasyObjectPool.instance.ReturnObjectToPool(gameObject);
            character.range.onTarget = false;
        }
    }
}
