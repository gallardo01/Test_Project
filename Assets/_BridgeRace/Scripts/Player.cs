using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player : ColorObject
{
    [SerializeField] protected float speed;
    [SerializeField] protected Transform parent;
    [SerializeField] protected LayerMask walkable, stairLayer, bridgeLayer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform body;

    protected IObjectPool<Brick> objectPool;
    protected bool canMove;
    protected RaycastHit hit;
    protected string currentAnimation;
    protected Stair stair;
    protected int level;
    protected Vector3 direction;

    public int Level { get => level; set => level = value; }
    public Transform Parent { get => parent; }

    protected static List<ColorType> usedColors;

    protected void Init()
    {
        level = 0;
        usedColors = usedColors ?? new List<ColorType>(GameManager.Ins.UsedColors);

        int color = Random.Range(0, usedColors.Count);
        ChangeColor(usedColors[color]);
        usedColors.RemoveAt(color);

        currentAnimation = "Idle";
        objectPool = ObjectPool.Instance.Pool;
    }

    public void ChangeAnim(string newAnimation)
    {
        animator.ResetTrigger(currentAnimation);
        currentAnimation = newAnimation;
        animator.SetTrigger(currentAnimation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick" && other.GetComponent<Brick>().ColorType == ColorType) BrickTrigger();
    }

    protected void BrickTrigger()
    {
        Brick brick = objectPool.Get();
        brick.ChangeColor(ColorType);
        brick.transform.parent = parent;
        brick.transform.SetPositionAndRotation(parent.position, parent.rotation);
        brick.transform.position += parent.transform.up * 0.25f * parent.childCount;
        brick.Collider.enabled = false;
    }

    protected void Reset() {
        direction = Vector3.zero;
        stair = null;
    }

    protected void Check() {
        // Check stair
        Physics.Raycast(transform.position + direction * speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, stairLayer);

        // Update stair color
        if (hit.collider != null)
        {
            stair = hit.collider.gameObject.GetComponent<Stair>();
            if (parent.childCount > 0 && (!stair.Filled() || stair.color != colorType))
            {
                stair.Fill(colorType);

                // Remove brick from stack
                parent.GetChild(parent.childCount - 1).gameObject.GetComponent<Brick>().Deactivate();
                parent.GetChild(parent.childCount - 1).transform.parent = null;
            }
        }

        // Check for moving
        if (!Physics.Raycast(transform.position + direction * speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, walkable))
            Physics.Raycast(transform.position + direction * speed * Time.deltaTime + Vector3.up, Vector3.down, out hit, 2f, bridgeLayer);
        
        canMove = false;

        canMove = 
        !stair || // No stair
        stair.Filled() && stair.color == colorType || // Filled stair with color
        direction.z < 0; // Move down

        // if (hit.collider != null && canMove) {
        //     transform.position = hit.point;
        // }
    }
}
