using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MoveNode : MonoBehaviour
{
    [SerializeField] private List<MoveNode> connectedNodes = new();
    public bool isTeleport;

    public MoveNode GetNextNode()
    {
        return connectedNodes[Random.Range(0, connectedNodes.Count)];
    }

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (connectedNodes.Count > 0)
        {
            foreach (var node in connectedNodes)
            {
                Gizmos.DrawLine(node.transform.position, transform.position);
            }
        }
    }
#endif
}
