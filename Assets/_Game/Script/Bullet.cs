using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Ban den dau
    [SerializeField] Transform target;

    // Nguoi ban ra vien dan nay
    [SerializeField] Character character;
    [SerializeField] float speed = 6f;
    [SerializeField] Transform child;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);
        child.Rotate(Vector3.up * -6, Space.Self);
    }

    public void OnInit(Character character, Transform target)
    {
        this.character = character;
        this.target = target;
        transform.forward = (target.position - transform.position).normalized;
        //transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
