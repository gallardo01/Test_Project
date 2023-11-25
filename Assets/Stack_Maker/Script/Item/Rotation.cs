using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 30f;

    void Update()
    {
        // Quay vật thể quanh trục Y
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
