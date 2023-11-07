using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot : MonoBehaviour
{

    [SerializeField] private float speedBot;
    [SerializeField] private Animator Anim;
    [SerializeField] private Rigidbody2D rb;
     private GameObject gameController;
     private GameObject[] path;
    private int step=0;
    private string currentAnim;
    private bool isMoving = true;
    

    // Start is called before the first frame update
    void Start()
    {       
         
        gameController = GameObject.FindGameObjectWithTag("GameController");
        path = gameController.GetComponent<gameController>().returnPath(Random.Range(0, 2));
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {

            transform.position = Vector3.MoveTowards(transform.position, path[step].transform.position, speedBot * Time.deltaTime);
            if (Vector3.Distance(transform.position, path[step].transform.position) < 0.1f)
            {
                step++;
                if (Vector3.Distance(transform.position, path[4].transform.position) < 0.1f)
                {
                    gameController.GetComponent<gameController>().increaseScore();
                    Destroy(gameObject);
                }

            }
        }
    }
    private void OnDespawn()
    {
        Destroy(gameObject);
    }

    public void ChangeAnim(string nameAnim)
    {
        if (currentAnim != nameAnim)
        {
            
            Anim.ResetTrigger(nameAnim);
            currentAnim = nameAnim;
            Anim.SetTrigger(currentAnim);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.tag == "Bullet")
        {
            isMoving = false;
            ChangeAnim("die");
            Invoke(nameof(OnDespawn), 1f);
        }
    }
}
