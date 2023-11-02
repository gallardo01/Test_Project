using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looping : MonoBehaviour
{
    public GameObject[] points;
    public float speed;
    int step=0;
    private Animator anim;
    private string curanim;
    private bool isRunning;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        ChangeAnim("Idle");
        speed = 1 - speed;
        StartCoroutine(Delay());
    }

    // Update is called once per frame
    void Update()
    {
        ChangeAnim("Run");
        transform.position = Vector3.MoveTowards(transform.position, points[step].transform.position, speed * Time.deltaTime);
        if (transform.position == points[step].transform.position)
        {
            step++;
            if (step == points.Length)
            {
                step = 0;
            }
        }   
    }

    private void ChangeAnim(string AnimName)
    {
        if (curanim != AnimName)
        {
            anim.ResetTrigger(AnimName);

            curanim = AnimName;

            anim.SetTrigger(AnimName);
        }
    }
}
