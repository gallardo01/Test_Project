using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Player : ColorObject
{
    [SerializeField] protected float speed;
    [SerializeField] protected Transform parent;
    [SerializeField] protected LayerMask walkable, stairLayer;
    [SerializeField] protected Animator animator;
    [SerializeField] protected Transform body;

    protected IObjectPool<Brick> objectPool;
    protected RaycastHit hit;
    protected string currentAnimation;
    protected Stair stair;
    protected int level;
    protected Vector3 direction;

    public int Level { get => level; set => level = value; }

    protected static List<ColorType> usedColors;

    // Start is called before the first frame update
    protected void Init()
    {
        if (usedColors == null)
        {
            usedColors = new List<ColorType>(GameManager.Ins.UsedColors);
            int color = Random.Range(0, usedColors.Count);
            ChangeColor(usedColors[color]);
            usedColors.RemoveAt(color);
        }
    }

    protected void ChangeAnim(string newAnimation) {
        animator.ResetTrigger(currentAnimation);
        currentAnimation = newAnimation;
        animator.SetTrigger(currentAnimation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick" && other.GetComponent<Brick>().colorType == colorType) BrickTrigger();
    }

    protected void BrickTrigger()
    {
        Brick brick = objectPool.Get();
        brick.ChangeColor(colorType);
        brick.transform.parent = parent;
        brick.transform.SetPositionAndRotation(parent.position, parent.rotation);
        brick.transform.position += parent.transform.up * 0.25f * parent.childCount;
        brick.Collider.enabled = false;
    }
}
