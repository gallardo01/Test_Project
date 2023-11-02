using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;
    private int WayPointIndex = 0;

    public float timeStop = 0f;
    private float timeDelay = 0f;
    private bool status = true;
    bool breakTime;
    // Start is called before the first frame update
    void Start()
    {
        target = WayPoints.points[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        if (status)
        {
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        }

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            Debug.Log("Toi diem:" + target.name);
            if (breakTime == false)
            {
                timeStop = Time.time + 2f;//thoi gian bat dau khi toi diem + 2f
                breakTime = true;
                status = false;
            }

            if (WayPointIndex >= WayPoints.points.Length - 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                if (Time.time > timeStop)
                {
                    GetNextWayPoint();

                    breakTime = false;
                    status = true;
                }

            }
        }


    }//End UPdate

    void GetNextWayPoint()
    {
        target = WayPoints.points[WayPointIndex];
        WayPointIndex++;
    }
}
