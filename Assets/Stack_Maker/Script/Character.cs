using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Transform playerBrichPrefab;
    public Transform boxBrick;
    public List<Transform> playerBricks = new List<Transform>();
    [SerializeField] private Animator Anim;



    private int currentAnim;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {
        clearBrick();
    }

    public virtual void OnDespawn()
    {

    }
    public void clearBrick()
    {
        for (int i = 0; i < playerBricks.Count; i++)
        {
            Destroy(playerBricks[i].gameObject);
        }
        playerBricks.Clear();
    }
    public void changAnim(int newAnim)
    {
        Anim.SetInteger("swap", newAnim);
    }
}
