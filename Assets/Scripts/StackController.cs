using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform brickParent;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private GameObject model;

    private float position;

    // Start is called before the first frame update
    void Start()
    {
        position = -0.35f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(start.position, Vector3.down, out hit, 1f, brickLayer)) {
            GameObject brick = Instantiate(brickPrefab, brickParent);
            brick.transform.localPosition = new Vector3(0, position, 0);
            position += 0.25f;
            hit.collider.gameObject.SetActive(false);
        } 

        model.transform.localPosition = new Vector3(0, position - 0.25f, 0);  
    }
}
