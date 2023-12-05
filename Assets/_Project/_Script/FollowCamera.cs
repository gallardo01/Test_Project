using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
    [ExecuteAlways]
#endif
public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping = 10f;
    
    private void Update()
    {
#if UNITY_EDITOR
        if (Application.IsPlaying(gameObject))
        {
            if (target)
                transform.position = Vector3.Lerp(transform.position, target.position + offset, damping * Time.deltaTime);
        }
        else
        {
            if (target) 
                transform.position = target.position + offset;
        }
#else
        if (target)
                transform.position = Vector3.Lerp(transform.position, target.position + offset, damping * Time.deltaTime);
#endif
    }


}
