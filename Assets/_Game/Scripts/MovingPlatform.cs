using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform aPoint, bPoint;
    [SerializeField] private float speed;
    private Vector3 target;

    private void Start() {
        target = bPoint.position;
        transform.position = aPoint.position;
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, aPoint.position) < 0.1f) {
            target = bPoint.position;
        } else if (Vector3.Distance(transform.position, bPoint.position) < 0.1f) {
            target = aPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Player") {
            other.transform.SetParent(null);
        }
    }
}
