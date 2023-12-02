using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : ColorObject
{

    [SerializeField] private float speed;
    [SerializeField] private Transform parent;
    [SerializeField] private LayerMask walkable, brick;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform body;
    [SerializeField] private Renderer skin;

    private IObjectPool<Brick> objectPool;
    private RaycastHit hit;
    private string currentAnimation;
    private Stair stair;

    // Start is called before the first frame update
    void Start()
    {
        currentAnimation = "Idle";
        objectPool = ObjectPool.Instance.Pool;
    }

    private void ChangeAnim(String newAnimation) {
        animator.ResetTrigger(currentAnimation);
        currentAnimation = newAnimation;
        animator.SetTrigger(currentAnimation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick") BrickTrigger();
    }

    private void BrickTrigger()
    {
        Brick brick = objectPool.Get();
        brick.transform.parent = parent;
        brick.transform.SetPositionAndRotation(parent.position, parent.rotation);
        brick.transform.position += Vector3.up * 0.25f * parent.childCount;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ChangeAnim("Run");
        }
        if (Input.GetMouseButtonUp(0)) {
            ChangeAnim("Idle");
        }

        stair = null;

        if (JoystickControl.direct != Vector3.zero) {
            body.forward = JoystickControl.direct;
            Physics.Raycast(transform.position + JoystickControl.direct * speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, brick);
            if (hit.collider != null) 
            {
                stair = hit.collider.gameObject.GetComponent<Stair>();
                if (parent.childCount > 0 && !stair.Filled()) {
                    parent.GetChild(parent.childCount - 1).gameObject.GetComponent<Brick>().Deactivate();
                }
            }
            Physics.Raycast(transform.position + JoystickControl.direct * speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, walkable);
        }
        if (hit.collider != null && (!stair || stair.Filled() || hit.point.y < transform.position.y)) {
            transform.position = hit.point;
        }
    }

}
