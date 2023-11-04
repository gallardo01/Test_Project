using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject start;
    public GameObject end;
    private Vector3 tmp;
    public LayerMask Enermy;
    private void Start()
    {
        tmp = start.transform.position;
        gameObject.transform.position = tmp;
    }
    private void OnDisable()
    {
        gameObject.transform.position = start.transform.position;
    }
    void Update()
    {
        if (transform.position == start.transform.position)
        {
            tmp = end.transform.position;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (transform.position == end.transform.position)
        {
            tmp = start.transform.position;
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, tmp, 0.007f);
        Check();
    }
    private void Check()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.00001f, Enermy);
        if (hit.collider != null)
        {
            hit.collider.gameObject.SetActive(false);
        }
    }
}
