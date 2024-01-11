using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    [SerializeField] float angle = -1f;

    private void Update()
    {
        transform.Rotate(Vector3.up * angle, Space.Self);
    }
}
