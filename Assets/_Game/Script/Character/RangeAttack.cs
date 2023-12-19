using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    public int segments = 360;
    public float radius = 5f;
    

    [SerializeField] private LineRenderer line;

    void Awake()
    {
        //line.widthMultiplier = 0.3f;
        line.startWidth = 0.3f;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        Draw();
    }

    void Draw()
    {
        float angle = 0f;
        for (int i = 0; i < (segments + 1); i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius * 0.83f;
            float z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius * 0.83f;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }
}
