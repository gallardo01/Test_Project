using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedBullet;
    private Transform start;
    private Transform end;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        start = gameController.GetComponent<gameController>().getStart();
        end = gameController.GetComponent<gameController>().getEnd();
        transform.position = start.position;
        target = end.position;
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


   
}
