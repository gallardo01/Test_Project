using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Camera camera;
    [SerializeField] private Player player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = Vector3.Lerp(camera.transform.position, player.transform.position + offset, Time.deltaTime * speed);
    }
}
