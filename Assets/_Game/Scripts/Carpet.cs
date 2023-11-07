using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Rigidbody2D rb1;
    [SerializeField] private Transform S;
    private float horizontal, vertical;
    [SerializeField] private float speed = 500f;
    private void OnEnable()
    {
        rb.position = S.position;
    }
    
    void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal) > 0.1f) {
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        
        rb.velocity = new Vector2(horizontal * Time.fixedDeltaTime * speed, rb.velocity.y);
        rb.velocity = new Vector2(rb.velocity.x, vertical * Time.fixedDeltaTime * speed);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
