using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{

    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private float speedBullet;


    Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = start.transform.position;
        target = end.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speedBullet * Time.deltaTime);

        if (Vector2.Distance(transform.position, end.transform.position) < 0.1f)
        {
            target = start.transform.position;
        }
        if (Vector2.Distance(transform.position, start.transform.position) < 0.1f)
        {
            target = end.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "bot")
        {
            Destroy(collision.gameObject);
            
        }
    }

   
}
