using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float waitTime = 1f;
    public GameObject start;
    public List<Transform> path;

    private int _currentTargetIndex = 0;

    // Update is called once per frame
    void Update()
    {
        if (_currentTargetIndex == -1) return;
        
        transform.position = Vector3.MoveTowards(
                transform.position,
                path[_currentTargetIndex].position,
                movementSpeed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, path[_currentTargetIndex].position) < 0.01f)
        {
            if (_currentTargetIndex == path.Count - 1)
            {
                StartCoroutine(RestartPath());
                return;
            }
            
            _currentTargetIndex++;
        }
    }

    IEnumerator RestartPath()
    {
        _currentTargetIndex = -1;
        gameObject.transform.position = start.transform.position;

        yield return new WaitForSeconds(waitTime);

        _currentTargetIndex = 0;
    }

    private float absNumber(float x, float y)
    {
        if (x - y > 0)
        {
            return x - y;
        }
        return y - x;
    }

    // Lan dau tien khi va cham

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "End")
        {
            gameObject.transform.position = start.transform.position;
        }
    }

    // Update - co dieu kien - 2 vat va cham
    void OnTriggerStay2D(Collider2D col)
    {

    }

    // 1 lan khi ma k va cham
    void OnTriggerExit2D(Collider2D col)
    {

    }

}
