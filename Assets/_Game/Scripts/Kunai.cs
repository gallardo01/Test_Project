using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hitVFX;
    public Rigidbody2D rb;
    
    void Start()
    {
        
        OnInit();
    }

    
    public void OnInit()
    {
        rb.velocity = transform.right * 15f;
        Invoke(nameof(OnDespawn), 4f);
    }
    void OnDespawn()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy") { 
            collision.GetComponent<Character>().OnHit(30f);
            GameController1.Instance.HitVFX = Instantiate(hitVFX, transform.position, transform.rotation);
            GameController1.Instance.hitVFX();
            OnDespawn();
        }
    }
}
