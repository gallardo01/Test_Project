using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField] float angle = -6f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.Rotate(Vector3.up * angle, Space.Self);
    }
}
