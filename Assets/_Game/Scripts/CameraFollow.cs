using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] Transform camera;
    [SerializeField] Vector3 offset;
    [SerializeField] Transform character;
    [SerializeField] float speed;

    void Start()
    {
        character = FindObjectOfType<Character>().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        camera.position = Vector3.Lerp(camera.position, character.position + offset, speed * Time.deltaTime);
    }
}
