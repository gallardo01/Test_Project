using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] private Transform camera;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform character;
    [SerializeField] private float speed;
    [SerializeField] private float increaseRatio;

    void Start()
    {
        character = FindObjectOfType<Character>().transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        camera.position = Vector3.Lerp(camera.position, character.position + offset, speed * Time.deltaTime);
    }

    public void UpSize() {
        Camera.main.orthographicSize *= (100 + increaseRatio) / 100;   
    }
}
