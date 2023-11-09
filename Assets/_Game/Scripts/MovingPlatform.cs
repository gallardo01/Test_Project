using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform aPoint, bPoint;
    [SerializeField] private float speed;
    private Vector3 target;
    private Collider2D _collider;

    private void Start() {
        _collider = GetComponent<Collider2D>();
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
        double height = other.collider.bounds.size.y;
        double collider_height = _collider.bounds.size.y;

        if (other.gameObject.tag == "Player" && MathF.Abs((float) (other.transform.position.y - height / 2 - (transform.position.y + collider_height / 2))) < 0.1) {
            other.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other) {

        if (other.gameObject.tag == "Player") {
            other.transform.SetParent(null);
        }
    }
}
