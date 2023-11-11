using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject hit_VFX;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();      
    }

    public void OnInit()
    {
        rb.velocity = transform.right * 10f;
        Invoke(nameof(OnDespawn), 5f);
    }

    public void OnDespawn()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().OnHit(30f);
            Instantiate(hit_VFX, transform.position, Quaternion.identity);
            OnDespawn();
        }
    }
}
