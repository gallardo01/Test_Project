using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    private void Update()
    {
        if(Physics.Raycast(transform.position,Vector3.up,playerLayer))
        {
            //Debug.Log(true);
            GameController.Instance.Score++;
            Destroy(gameObject);
        }
    }
}
