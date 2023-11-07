using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroom : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform point1, point2;
    private Vector3 target;
    private Vector3 pointStart;


    private void Start()
    {
        target = point1.position;
        pointStart = transform.position; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, point1.position) < 0.1f)
        {
            target = point2.position;
        }
        else if (Vector2.Distance(transform.position, point2.position) < 0.1f)
        {
            target = point1.position;
        }

    }
    public void SpawnEnemy()
    {
        
        gameObject.SetActive(true);
        transform.position = pointStart;
    }
}
