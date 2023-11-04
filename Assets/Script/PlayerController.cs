using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveChangeRate;
    [SerializeField] private Animator animator;

    private Vector3 originalPosition, move, newMove;
    // private Collider2D target;
    private int randomDirection;
    private float originalSpeed, time;
    private string currentAnim;

    // Start is called before the first frame update
    void Start()
    {
        currentAnim = "isRun";
        originalPosition = transform.position;
        move = Vector3.up;
        newMove = move;
     
        originalSpeed = speed;
     
        randomDirection = 1;
        CalculateTime();
        // target = one;

        Invoke(nameof(Stop), 1);

        // StartCoroutine(StopPerSecond());
    }

    private void ChangeAnim(string newAnim) {
        animator.ResetTrigger(currentAnim);
        currentAnim = newAnim;
        animator.SetTrigger(currentAnim);
    }

    private void OnEnable() {
        animator.SetTrigger("isRun");
        move = Vector3.up;
        newMove = Vector3.up;
    }

    private void Death() {
        gameObject.SetActive(false);
    }

    IEnumerator StopPerSecond() {
        yield return new WaitForSecondsRealtime(1);
        speed = speed == 0 ? originalSpeed : 0;
        StartCoroutine(StopPerSecond());
    }

    // Update is called once per frame
    void Update()
    {
        // move = Vector3.zero;
        
        // if (Input.GetKey(KeyCode.W)) move += Vector3.up; 
        // if (Input.GetKey(KeyCode.A)) move += Vector3.left; 
        // if (Input.GetKey(KeyCode.S)) move += Vector3.down; 
        // if (Input.GetKey(KeyCode.D)) move += Vector3.right;

        Curve();

    }

    private void FixedUpdate() {

        // Vector
        rb.MovePosition(transform.position + move.normalized * speed * Time.deltaTime);

        // Move Toward
        // rb.MovePosition(Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime));
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.tag == "Bullet") {
            GameController.Instance.IncreaseScore();
            ChangeAnim("isDeath");
            animator.speed = 1;
            Invoke(nameof(Death), 0.75f);
        }

        // Vector
        if (other.tag == "one") {
            randomDirection = Random.Range(0, 2) * 2 - 1;
            newMove = Quaternion.Euler(0, 0, -90 * randomDirection) * move;
        }
        else if (other.tag == "two") newMove = Quaternion.Euler(0, 0, 90 * randomDirection) * move;
        else if (other.tag == "three") newMove = Quaternion.Euler(0, 0, 90 * randomDirection) * move;
        else if (other.tag == "four") newMove = Quaternion.Euler(0, 0, -90 * randomDirection) * move;
        else if (other.tag == "Finish") {
            transform.position = originalPosition;
            gameObject.SetActive(false);
        }


        // Move Toward
        // if (other == one) target = two;
        // else if (other == two) target = three;
        // else if (other == three) target = four;
        // else if (other == four) transform.position = originalPosition;

    }

    private void Curve() {
        move = Vector3.Lerp(move, newMove, moveChangeRate * Time.deltaTime);
    }

    private void Stop() {
        speed = 0;
        if (currentAnim == "isRun") animator.speed = 0;
        
        CalculateTime();

        Invoke(nameof(Move), time);
    }

    private void Move() {
        speed = originalSpeed;        
        animator.speed = 1;

        CalculateTime();

        Invoke(nameof(Stop), time);
    }

    private void CalculateTime() {
        time = Random.Range(1f, 3f);
        time = time > 2 ? 2 : time;
    }
}
