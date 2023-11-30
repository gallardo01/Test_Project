using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private LayerMask walkable, brick;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform body;
    [SerializeField] private Renderer skin;
    [SerializeField] private List<Color> colors;
        
    private RaycastHit hit;
    private String currentAnimation;
    private Stair stair;
    private Color color;

    // Start is called before the first frame update
    void Start()
    {
        currentAnimation = "Idle";
        int c = UnityEngine.Random.Range(0, colors.Count);
        color = colors[c];
        colors.RemoveAt(c);
        skin.material.color = color;
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
        GameObject brick = Instantiate(brickPrefab, parent);
        brick.transform.localPosition = Vector3.zero;
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
                    stair.Fill(color);
                    Destroy(parent.GetChild(parent.childCount - 1).gameObject);
                }
            }
            Physics.Raycast(transform.position + JoystickControl.direct * speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, walkable);
        }
        if (hit.collider != null && (!stair || stair.Filled() || hit.point.y < transform.position.y)) {
            transform.position = hit.point;
        }
    }

}
