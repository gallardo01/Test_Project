using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Name : MonoBehaviour
{
    public TextMeshProUGUI nameChar;
    public Transform target;
     
     
    private void Update()
    {
        if (target != null && Camera.main != null && this.transform != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(target.transform.position);
            this.transform.position = pos;
        }
        
    }
}
