using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Bullet : MonoBehaviour
{
    // Ban den dau
    private Transform target;

    // Nguoi ban ra vien dan nay
    private Character character;
    [SerializeField] float speed = 8f;
    [SerializeField] Transform child;

    CounterTime counterTime = new CounterTime();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6, Space.Self);
        counterTime.Execute();
    }

    public void OnInit(Character character, Transform target)
    {
        this.character = character;
        this.target = target;
        transform.forward = (target.position - transform.position).normalized;
        //transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        counterTime.Start(deactiveBullet, 1f);
    }

    public void deactiveBullet()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject != character.gameObject)
        {
            SoundManager.Ins.PlayOneShot(0);
            character.UpdatePoints();
            other.GetComponent<Character>().OnDeath();
            character.RemoveTarget(target.GetComponent<Character>());
            gameObject.SetActive(false);
        }
    }


}
