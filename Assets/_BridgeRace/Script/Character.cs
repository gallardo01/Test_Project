using MarchingBytes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Dependencies.Sqlite.SQLite3;

public class Character : ColorObject
{
    // Start is called before the first frame update
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected LayerMask stairLayer;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected GameObject brickParent;
    protected GameObject brick;
    [SerializeField] protected GameObject body;
    protected List<GameObject> brickObject = new List<GameObject>();
    [SerializeField] protected int brickCount = 0;
    public int BrickCount => brickCount;
    public Animator animator;
    protected string currentAnim;
    protected Vector3 nextPoint;
    public Stage stage;
    
    public IEnumerator spawnBrick()
    {
        yield return new WaitForSeconds(5f);
        stage.NewBrick(colorType);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick")
        {
            if (other.GetComponent<Brick>().colorType == colorType)
            {
                brick = other.gameObject;
                stage.RemoveBrick(brick.GetComponent<Brick>());
                brick.GetComponent<BoxCollider>().enabled = false;
                brick.GetComponent<Brick>().ChangeColor(colorType);
                brick.transform.SetParent(brickParent.transform);
                brick.transform.position = brickParent.transform.position + new Vector3(0, brickCount * 0.2f, 0);
                brick.transform.rotation = body.transform.rotation;
                brickObject.Add(brick);
                brickCount++;
                StartCoroutine(spawnBrick());
            }
        }
    }
    public Vector3 check(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint + 0.5f * Vector3.up, Vector3.down, out hit, 3f, groundLayer))
        {
            return hit.point + new Vector3(0, 1f, 0);
        }
        return nextPoint;
    }
    public void ReMoveBrick()
    {
        brickCount--;
        EasyObjectPool.instance.ReturnObjectToPool(brickObject[brickObject.Count - 1]);
        brickObject[brickObject.Count - 1].SetActive(false);
        brickObject[brickObject.Count - 1].GetComponent<BoxCollider>().enabled = true;
        brickObject[brickObject.Count - 1].transform.SetParent(null);
        brickObject.RemoveAt(brickObject.Count - 1);
    }
    public bool CanMove(Vector3 point)
    {
        bool canMove = false;
        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, 2f, groundLayer))
        {
            if (Physics.Raycast(point, Vector3.up, out hit, 7f, stairLayer))
            {
                if (hit.collider.GetComponent<Stair>().colorType != colorType && brickCount > 0)
                {
                    hit.collider.GetComponent<Stair>().ChangeColor(colorType);
                    ReMoveBrick();
                    canMove = true;
                }
                else if (hit.collider.GetComponent<Stair>().colorType == colorType)
                {
                    canMove = true;
                }
            }
            else canMove = true;
        }

        return canMove;
    }
    public void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            if(currentAnim!=null) animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }
}
